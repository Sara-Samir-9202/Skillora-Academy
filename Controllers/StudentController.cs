using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;
using StudentSystem.ViewModels;

namespace StudentSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ===================== INDEX =====================
        public IActionResult Index()
        {
            var students = _context.Students
                .Include(s => s.Department)
                .ToList();

            var vm = _mapper.Map<List<StudentVM>>(students);

            ViewBag.Courses = _context.Courses.ToList();

            return View(vm);
        }

        // ===================== DETAILS =====================
        public IActionResult Details(int id)
        {
            var student = _context.Students
                .Include(s => s.Department)
                .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                .FirstOrDefault(s => s.SSN == id);

            if (student == null) return NotFound();

            var vm = new StudentDetailsVM
            {
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                Address = student.Address,
                Image = student.Image,
                DepartmentName = student.Department?.Name,
                Courses = student.StudentCourses.Select(sc => new CourseDegreeVM
                {
                    CourseName = sc.Course.Name,
                    Degree = sc.Degree
                }).ToList()
            };

            return View(vm);
        }

        // ===================== CREATE (GET) =====================
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        // ===================== CREATE (POST) =====================
        [HttpPost]
        public IActionResult Create(Student student, IFormFile file,
            List<int> CourseIds, List<int> Degrees)
        {
            ModelState.Remove("Department");
            ModelState.Remove("StudentCourses");

            bool nameExists = _context.Students.Any(s => s.Name == student.Name);
            if (nameExists)
                ModelState.AddModelError("Name", "This name already exists");

            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName).ToLower();
                if (ext != ".jpg" && ext != ".png")
                    ModelState.AddModelError("Image", "Only .jpg or .png files are allowed");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _context.Departments.ToList();
                ViewBag.Courses = _context.Courses.ToList();
                return View(student);
            }

            if (file != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                    file.CopyTo(stream);

                student.Image = fileName;
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            if (CourseIds != null)
            {
                for (int i = 0; i < CourseIds.Count; i++)
                {
                    _context.StudentCourses.Add(new StudentCourse
                    {
                        StudentId = student.SSN,
                        CourseId = CourseIds[i],
                        Degree = Degrees[i]
                    });
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Success", new { id = student.SSN });
        }

        // ===================== SUCCESS =====================
        public IActionResult Success(int id)
        {
            var student = _context.Students
                .Include(s => s.Department)
                .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                .FirstOrDefault(s => s.SSN == id);

            if (student == null) return NotFound();

            return View(student);
        }

        // ===================== EDIT (GET) =====================
        public IActionResult Edit(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.SSN == id);

            if (student == null) return NotFound();

            ViewBag.Departments = _context.Departments.ToList();

            return View(student);
        }

        // ===================== EDIT (POST) =====================
        [HttpPost]
        public IActionResult Edit(Student student, IFormFile? file)
        {
            ModelState.Remove("Department");
            ModelState.Remove("StudentCourses");

            var existing = _context.Students.FirstOrDefault(s => s.SSN == student.SSN);

            if (existing == null) return NotFound();

            bool nameExists = _context.Students
                .Any(s => s.Name == student.Name && s.SSN != student.SSN);
            if (nameExists)
                ModelState.AddModelError("Name", "This name already exists");

            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName).ToLower();
                if (ext != ".jpg" && ext != ".png")
                    ModelState.AddModelError("Image", "Only .jpg or .png files are allowed");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _context.Departments.ToList();
                return View(student);
            }

            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.Phone = student.Phone;
            existing.Address = student.Address;
            existing.Age = student.Age;
            existing.DeptId = student.DeptId;

            if (file != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                    file.CopyTo(stream);

                existing.Image = fileName;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ===================== DELETE (GET) =====================
        public IActionResult Delete(int id)
        {
            var student = _context.Students
                .Include(s => s.Department)
                .FirstOrDefault(s => s.SSN == id);

            if (student == null) return NotFound();

            return View(student);
        }

        // ===================== DELETE (POST) =====================
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.SSN == id);

            if (student == null) return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}