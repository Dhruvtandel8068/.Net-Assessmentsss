using Microsoft.AspNetCore.Mvc;
using Assessment7.Data;        // DbContext namespace
using Assessment7.Models;      // Student model
using System.Linq;

namespace Assessment7.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // List all students
        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        // Create Student - GET
        public IActionResult Create()
        {
            return View();
        }

        // Create Student - POST
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
    }
}
