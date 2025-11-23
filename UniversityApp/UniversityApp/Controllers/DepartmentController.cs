using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;
using UniversityApp.Models;
using UniversityApp.ViewModels;
using System.Linq;

namespace UniversityApp.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // ========== Index ==========
        // /Department
        public IActionResult Index()
        {
            return RedirectToAction(nameof(ShowAll));
        }

        // ========== Show All ==========
        public IActionResult ShowAll()
        {
            var departments = _context.Departments
                                      .Include(d => d.Students)
                                      .ToList();
            return View(departments);
        }

        // ========== Details ==========
        public IActionResult ShowDetails(int id)
        {
            var dept = _context.Departments
                               .Include(d => d.Students)
                               .FirstOrDefault(d => d.Id == id);

            if (dept == null) return NotFound();

            var studentsAbove25 = dept.Students
                                      .Where(s => s.Age > 25)
                                      .Select(s => s.Name)
                                      .ToList();

            string state = dept.Students.Count > 50 ? "Main" : "Branch";

            var viewModel = new DepartmentDetailsVM
            {
                DepartmentName = dept.Name,
                StudentNamesAbove25 = studentsAbove25,
                DepartmentState = state
            };

            return View(viewModel);
        }

        // ========== Add ==========
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Department dept)
        {
            if (!ModelState.IsValid) return View(dept);

            _context.Departments.Add(dept);
            _context.SaveChanges();
            return RedirectToAction(nameof(ShowAll));
        }
    }
}
