using Microsoft.AspNetCore.Mvc;
using Assessment5.Models;
using System.Collections.Generic;

namespace Assessment5.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Dhruv", Marks = 85 },
                new Student { Id = 2, Name = "Yash", Marks = 72 },
                new Student { Id = 3, Name = "Krish", Marks = 90 }
            };

            return View(students);
        }
    }
}
