//using Assingment_2MVC.Data;
//using Assingment_2MVC.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using System.Linq; 

//namespace Assingment_2MVC.Controllers
//{
//    public class StudentsController : Controller
//    {
//        private RegistrationContext _context;

//        public StudentsController(RegistrationContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            var students = _context.Students.Include(s => s.Course).ToList();
//            return View(students);
//        }

//        // GET: Students/Details/5
//        public IActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var student = _context.Students
//                .Include(s => s.Course)
//                .FirstOrDefault(m => m.Id == id);

//            if (student == null)
//            {
//                return NotFound();
//            }

//            return View(student);
//        }


//        // GET: Students/Create
//        public IActionResult Create()
//        {
//            // Retrieve the list of courses from the database
//            var courses = _context.Courses.ToList();

//            // Set the list of courses in ViewBag for the dropdown in the Create view
//            ViewBag.Courses = new SelectList(courses, "Id", "Name");

//            return View();
//        }

//        // POST: Students/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create([Bind("Name,Email,Status,CourseId")] Student student)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(student);
//                _context.SaveChanges();
//                return RedirectToAction(nameof(Index));
//            }

//            // If the model state is not valid, retrieve the list of courses again for the view
//            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", student.CourseId);
//            return View(student);
//        }

//        // GET: Students/Edit/5
//        public IActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var student = _context.Students.Find(id);

//            if (student == null)
//            {
//                return NotFound();
//            }

//            // Retrieve the list of courses to populate a dropdown in the view
//            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", student.CourseId);
//            return View(student);
//        }
//        // POST: Students/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(int? id, [Bind("Id,Name,Email,Status,CourseId")] Student student)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(student);
//                    _context.SaveChanges();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!StudentExists(id.Value))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }

//            // If the model state is not valid, retrieve the list of courses again for the view
//            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", student.CourseId);
//            return View(student);
//        }
//        // GET: Students/Delete/5
//        public IActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var student = _context.Students
//                .Include(s => s.Course)
//                .FirstOrDefault(m => m.Id == id);

//            if (student == null)
//            {
//                return NotFound();
//            }

//            return View(student);
//        }

//        // POST: Students/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            var student = _context.Students.Find(id);
//            _context.Students.Remove(student);
//            _context.SaveChanges();
//            return RedirectToAction(nameof(Index));
//        }


//        private bool StudentExists(int id)
//        {
//            return _context.Students.Any(e => e.Id == id);
//        }
//    }
//}
