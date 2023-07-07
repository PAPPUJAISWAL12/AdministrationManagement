using FeemanagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeemanagementSystem.Controllers
{
    public class AdminController : Controller
    {
		private readonly FeeManagementSystemContext _context;
        public AdminController(FeeManagementSystemContext context)
        {
            _context = context;
        }

		public IActionResult Index()
        {
            List<Student> students = _context.Students.ToList();
            ViewBag.StdCount=students.Count;
            return View();
        }
    }
}
