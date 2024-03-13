using Assingment_2MVC.Data;
using Assingment_2MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace Assingment_2MVC.Services
{
    public class CourseManagerService : ICourseManagerService
    {
        private readonly IConfiguration _configuration;

        private readonly RegistrationContext _registrationContext;

        public CourseManagerService(RegistrationContext registrationContext, IConfiguration configuration)
        {
            _registrationContext = registrationContext;
            _configuration = configuration;
        }

        public int AddCourse(Course Sub)
        {
           _registrationContext.Courses.Add(Sub);
            _registrationContext.SaveChanges();
            return Sub.CourseId;
        }

        public List<Course>? GetAllCources()
        {
            return _registrationContext.Courses
                .Include(e => e.Students)
                .OrderByDescending(e=> e.StartDate)
                .ToList();
        }

        public List<Student>? GetAllStudents(int courseId)
        {
            return _registrationContext.Students
                 .Where(s => s.CourseId == courseId)
                 .ToList();
        }

        public Course? GetCourseById(int id)
        {
            return _registrationContext.Courses.Include(e => e.Students).FirstOrDefault(e => e.CourseId == id);

        }
        public int AddStudent(Student student)
        {
            _registrationContext.Students.Add(student);
            _registrationContext.SaveChanges();

            return student.StudentId;
        }

        public Student? GetStudentById(int courseId, int studentId)
        {

        return _registrationContext.Students.Include(e=> e.Course).FirstOrDefault(e=>e.StudentId== studentId && e.CourseId== courseId);
        }

        public void UpdateConfirmationStatus(int courseId, int studentId, StudentStatus status)
        {
            var student = GetStudentById(courseId, studentId);

            if (student != null)
            {
                student.StudentStatus = status;
                _registrationContext.SaveChanges();
            }

        }

        public int UpdateCourse(Course updatedCourse)
        {
            if (updatedCourse == null)
            {
                // Optionally handle the case where updatedCourse is null
                return -1; // or some other sentinel value
            }

            var existingCourse = _registrationContext.Courses.Find(updatedCourse.CourseId);

            if (existingCourse == null)
            {
                // Optionally handle the case where the course with the given ID is not found
                return -1; // or some other sentinel value
            }

            // Update the existing course properties
            existingCourse.CourseName = updatedCourse.CourseName;
            existingCourse.CourseInstructor = updatedCourse.CourseInstructor;
            existingCourse.StartDate = updatedCourse.StartDate;
            existingCourse.RoomNumber = updatedCourse.RoomNumber;

            // Save changes to the database
            _registrationContext.SaveChanges();

            // Return the ID of the updated course
            return existingCourse.CourseId;
        }

        public void SentEnrollEmailWithCourseID(int courseId, string scheme, string host)
        {
            var course = GetCourseById(courseId);
            if (course?.Students == null) return;
            var student = course.Students.Where(s => s.StudentStatus == StudentStatus.ConfirmationMessageNotSent)
                .ToList();
            try
            {
                var smtpHost = _configuration["SmtpSettings:Host"];
                var smtpPort = _configuration["SmtpSettings:Port"];
                var fromAddress = _configuration["SmtpSettings:FromAddress"];
                var fromPassword = _configuration["SmtpSettings:FromPassword"];

                using var smtpClient = new SmtpClient(smtpHost);
                smtpClient.Port = string.IsNullOrEmpty(smtpPort) ? 587 : Convert.ToInt32(smtpPort);
                smtpClient.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtpClient.EnableSsl = true;
                foreach (var students in student)
                {
                    var responseUrl = $"{scheme}://{host}/courses/{courseId}/enroll/{students.StudentId}";

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(fromAddress),
                        Subject = $"[Action Required] Confirm : {students?.Course?.CourseName}, {students.Name}",
                        Body = CreateBody(students, responseUrl),
                        IsBodyHtml = true
                    };
                    if (students.Email == null) return;

                    mailMessage.To.Add(students.Email);

                    smtpClient.Send(mailMessage);

                    students.StudentStatus = StudentStatus.ConfirmationMessageSent;
                }
                _registrationContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string CreateBody(Student student, string responseUrl)
        {
            return $@"
                <h1>Hello, {student.Name}:</h1>
                <p>Your request to Enroll in the Course {student?.Course?.CourseName} in Room Number {student?.Course?.RoomNumber} starting {student?.Course?.StartDate:d} With Prof. {student?.Course?.CourseInstructor}
                </P>

                 <p>
                 
                 <a href={responseUrl}>confirm your enrollment</a>
               </p>
               <p>Sincerely,</p>
               <p>Course Manager App</p>
            ";
        }
    }
}
