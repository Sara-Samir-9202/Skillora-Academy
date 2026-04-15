using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;

namespace StudentSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= INDEX =================
        public IActionResult Index()
        {
            var courses = _context.Courses
                .Include(c => c.StudentCourses)
                .ToList();

            return View(courses);
        }

        // ================= VALIDATE DEGREES (REMOTE) =================
        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateDegrees(int? minDegree, int? maxDegree)
        {
            if (minDegree.HasValue && maxDegree.HasValue && minDegree >= maxDegree)
                return Json("Min Degree must be less than Max Degree");

            return Json(true);
        }

        // ================= CREATE (GET) =================
        public IActionResult Create()
        {
            return View();
        }

        // ================= CREATE (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
                return View(course);

            bool exists = _context.Courses.Any(c => c.Name == course.Name);
            if (exists)
            {
                ModelState.AddModelError("Name", "Course name must be unique");
                return View(course);
            }

            if (course.MinDegree.HasValue && course.MaxDegree.HasValue)
            {
                if (course.MinDegree >= course.MaxDegree)
                {
                    ModelState.AddModelError("MinDegree", "Min Degree must be less than Max Degree");
                    return View(course);
                }
            }

            _context.Courses.Add(course);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // ================= EDIT (GET) =================
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CrsId == id);

            if (course == null) return NotFound();

            return View(course);
        }

        // ================= EDIT (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            var existing = _context.Courses.FirstOrDefault(c => c.CrsId == course.CrsId);

            if (existing == null) return NotFound();

            bool exists = _context.Courses.Any(c =>
                c.Name == course.Name && c.CrsId != course.CrsId);

            if (exists)
            {
                ModelState.AddModelError("Name", "Course name must be unique");
                return View(course);
            }

            if (course.MinDegree.HasValue && course.MaxDegree.HasValue)
            {
                if (course.MinDegree >= course.MaxDegree)
                {
                    ModelState.AddModelError("MinDegree", "Min Degree must be less than Max Degree");
                    return View(course);
                }
            }

            existing.Name = course.Name;
            existing.Description = course.Description;
            existing.MinDegree = course.MinDegree;
            existing.MaxDegree = course.MaxDegree;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // ================= DELETE (GET) =================
        public IActionResult Delete(int id)
        {
            var course = _context.Courses
                .Include(c => c.StudentCourses)
                .FirstOrDefault(c => c.CrsId == id);

            if (course == null) return NotFound();

            return View(course);
        }

        // ================= DETAILS =================
        public IActionResult Details(int id)
        {
            var course = _context.Courses
                .Include(c => c.StudentCourses)
                    .ThenInclude(sc => sc.Student)
                .FirstOrDefault(c => c.CrsId == id);

            if (course == null) return NotFound();

            return View(course);
        }

        // ================= DELETE (POST) =================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CrsId == id);

            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}