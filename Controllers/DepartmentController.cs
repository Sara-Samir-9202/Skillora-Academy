using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;

namespace StudentSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===================== INDEX =====================
        public IActionResult Index()
        {
            var departments = _context.Departments
                .Include(d => d.Students)
                .ToList();

            return View(departments);
        }

        // ===================== CREATE (GET) =====================
        public IActionResult Create()
        {
            return View();
        }

        // ===================== CREATE (POST) =====================
        [HttpPost]
        public IActionResult Create(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ===================== EDIT (GET) =====================
        public IActionResult Edit(int id)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.DeptId == id);

            if (dept == null) return NotFound();

            return View(dept);
        }

        // ===================== EDIT (POST) =====================
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            var existing = _context.Departments.FirstOrDefault(d => d.DeptId == department.DeptId);

            if (existing == null) return NotFound();

            existing.Name = department.Name;
            existing.Location = department.Location;
            existing.PhoneNumber = department.PhoneNumber;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        // ================= DETAILS =================
        public IActionResult Details(int id)
        {
            var dept = _context.Departments
                .Include(d => d.Students)
                .FirstOrDefault(d => d.DeptId == id);

            if (dept == null) return NotFound();

            return View(dept);
        }
        // ===================== DELETE (GET) =====================
        public IActionResult Delete(int id)
        {
            var dept = _context.Departments
                .Include(d => d.Students)
                .FirstOrDefault(d => d.DeptId == id);

            if (dept == null) return NotFound();

            return View(dept);
        }

        // ===================== DELETE (POST) =====================
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.DeptId == id);

            if (dept == null) return NotFound();

            _context.Departments.Remove(dept);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}