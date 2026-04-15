using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;

namespace StudentSystem.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= INDEX =================
        public IActionResult Index()
        {
            var instructors = _context.Instructors
                .Include(i => i.Department)
                .ToList();

            return View(instructors);
        }

        // ================= DETAILS =================
        public IActionResult Details(int id)
        {
            var instructor = _context.Instructors
                .Include(i => i.Department)
                .Include(i => i.CourseInstructors)
                    .ThenInclude(ci => ci.Course)
                .FirstOrDefault(i => i.InsId == id);

            if (instructor == null) return NotFound();

            return View(instructor);
        }

        // ================= VALIDATE EMAIL (REMOTE) =================
        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateEmail(string email, int insId)
        {
            bool exists = _context.Instructors
                .Any(i => i.Email == email && i.InsId != insId);

            if (exists)
                return Json("This email already exists");

            return Json(true);
        }

        // ================= CREATE (GET) =================
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            return View();
        }

        // ================= CREATE (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Instructor instructor)
        {
            ModelState.Remove("Department");
            ModelState.Remove("CourseInstructors");

            bool emailExists = _context.Instructors
                .Any(i => i.Email == instructor.Email);

            if (emailExists)
                ModelState.AddModelError("Email", "This email already exists");

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _context.Departments.ToList();
                return View(instructor);
            }

            _context.Instructors.Add(instructor);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // ================= EDIT (GET) =================
        public IActionResult Edit(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.InsId == id);

            if (instructor == null) return NotFound();

            ViewBag.Departments = _context.Departments.ToList();

            return View(instructor);
        }

        // ================= EDIT (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Instructor instructor)
        {
            ModelState.Remove("Department");
            ModelState.Remove("CourseInstructors");

            var existing = _context.Instructors
                .FirstOrDefault(i => i.InsId == instructor.InsId);

            if (existing == null) return NotFound();

            bool emailExists = _context.Instructors
                .Any(i => i.Email == instructor.Email && i.InsId != instructor.InsId);

            if (emailExists)
                ModelState.AddModelError("Email", "This email already exists");

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _context.Departments.ToList();
                return View(instructor);
            }

            existing.Name = instructor.Name;
            existing.Age = instructor.Age;
            existing.Salary = instructor.Salary;
            existing.Degree = instructor.Degree;
            existing.Email = instructor.Email;
            existing.Address = instructor.Address;
            existing.DeptId = instructor.DeptId;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // ================= DELETE (GET) =================
        public IActionResult Delete(int id)
        {
            var instructor = _context.Instructors
                .Include(i => i.Department)
                .FirstOrDefault(i => i.InsId == id);

            if (instructor == null) return NotFound();

            return View(instructor);
        }

        // ================= DELETE (POST) =================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.InsId == id);

            if (instructor == null) return NotFound();

            _context.Instructors.Remove(instructor);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}