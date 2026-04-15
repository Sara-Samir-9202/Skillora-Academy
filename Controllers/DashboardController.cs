using Microsoft.AspNetCore.Mvc;
using StudentSystem.Models;

namespace StudentSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["ShowHero"] = true;
            return View("Dashboard");
        }
    }
}