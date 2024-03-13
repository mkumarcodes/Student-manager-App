using Assingment_2MVC.Models;
using Assingment_2MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Assingment_2MVC.Controllers
{
    public class CourseController : AbstractBaseController
    {
        private readonly ICourseManagerService _courseManagerService;

        public CourseController(ICourseManagerService courseManagerService)
        {
            _courseManagerService = courseManagerService;
        }

        [HttpGet("/courses")]
        public IActionResult List()
        {
            SetWelcome();

            var coursesViewModel = new CoursesViewModel()
            {
                courses = _courseManagerService.GetAllCources(),
            };
            

            return View(coursesViewModel);
        }

           [HttpGet("/courses/{id:int}/edit")]
        public IActionResult Edit(int id)
        {
            SetWelcome();

            var updatedCourse = _courseManagerService.GetCourseById(id);

            if (updatedCourse == null)
                return NotFound();


            var courseViewModel = new CourseViewModel
            {
                Course = _courseManagerService.GetCourseById(id),
            };

            return View(courseViewModel);
        }

        [HttpPost("/courses/{id:int}/edit")]
        public IActionResult Edit(int id, CourseViewModel CourseViewModel)
        {
            SetWelcome();

            if (!ModelState.IsValid) return View(CourseViewModel);

            _courseManagerService.UpdateCourse(CourseViewModel.Course);

            TempData["notify"] = $"{CourseViewModel.Course.CourseName} updated successfully!";
            TempData["className"] = "info";
            return RedirectToAction("Manage", new { id });
        }


        [HttpGet("/courses/{id:int}")]
        public IActionResult Manage(int id)
        {
            SetWelcome();

            var updatedCourse = _courseManagerService.GetCourseById(id);

            if (updatedCourse == null)
                return NotFound();

            var ManageCourseViewModel = new ManageCourseViewModel()
            {
                Course = updatedCourse,
                Student = new Student(),
                CountConfirmationMessageNotSent = updatedCourse.Students.Count(g => g.StudentStatus == StudentStatus.ConfirmationMessageNotSent),
                CountConfirmationMessageSent = updatedCourse.Students.Count(g => g.StudentStatus == StudentStatus.ConfirmationMessageSent),
                CountEnrollmentConfirmed = updatedCourse.Students.Count(g => g.StudentStatus == StudentStatus.EnrollmentConfirmed),
                CountEnrollmentDeclined = updatedCourse.Students.Count(g => g.StudentStatus == StudentStatus.EnrollmentDeclined),
                Students = _courseManagerService.GetAllStudents(id)

            };

            return View(ManageCourseViewModel);
        }


        [HttpPost("courses/{id:int}")]
        public IActionResult Manage(int id, ManageCourseViewModel managerViewModel)
        {
            SetWelcome();
            var course = _courseManagerService.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }

            //if (!ModelState.IsValid) return View(managerViewModel);

            var newStudent = managerViewModel.Student;
            newStudent.CourseId = id;

            // Add the new student using service
            _courseManagerService.AddStudent(newStudent);

            TempData["notify"] = $"{newStudent.Name} has added to the course successfully";
            TempData["className"] = "successfull";

            // Redirect to the same action to refresh the view
            return RedirectToAction("Manage", new { id });

        }

        public IActionResult SendEmail(int courseId)
        {
            _courseManagerService.SentEnrollEmailWithCourseID(courseId, Request.Scheme, Request.Host.ToString());
            return RedirectToAction("Manage", new { id = courseId });
        }

        [HttpGet("/courses/add")]
        public IActionResult Add()
        {
            SetWelcome();



            var CourseViewModel = new CourseViewModel()
            {
                Course = new Course()
            };

            return View(CourseViewModel);
        }

        [HttpPost("/courses/add")]
        public IActionResult Add(CourseViewModel CourseViewModel)
        {
            SetWelcome();
            if (!ModelState.IsValid)
            {
                return View(CourseViewModel);
            }
            _courseManagerService.AddCourse(CourseViewModel.Course);

            TempData["notify"] = $"{CourseViewModel.Course.CourseName} added successfully!";
            TempData["className"] = "success";
            return RedirectToAction("Manage", new { id = CourseViewModel.Course.CourseId });
        }

        [HttpGet("/thank-you/{response}")]
        public IActionResult ThankYou(string response)
        {
            SetWelcome();
            return View("ThankYou", response);
        }

        [HttpGet("/courses/{courseId:int}/enroll/{studentId:int}")]
        public IActionResult Enroll(int courseId, int studentId)
        {
            SetWelcome();

            var student = _courseManagerService.GetStudentById(courseId, studentId);

            if (student == null) return NotFound();

            var enrollVeiwModel = new EnrollViewModel
            {
                Student = student
            };
            return View(enrollVeiwModel);
        }

        [HttpPost("/courses/{courseId:int}/enroll/{studentId:int}")]
        public IActionResult Enroll(int courseId, int studentId, EnrollViewModel enrollViewModel)
        {
            SetWelcome();

            var student = _courseManagerService.GetStudentById(courseId, studentId);
            if (student == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                var status = enrollViewModel.Response == "Yes"
              ? StudentStatus.EnrollmentConfirmed
              : StudentStatus.EnrollmentDeclined;

                _courseManagerService.UpdateConfirmationStatus(courseId, studentId, status);

                return RedirectToAction("ThankYou", new { response = enrollViewModel.Response });
            }
            else
            {
                return View(enrollViewModel);
            }


        }
    }
}
