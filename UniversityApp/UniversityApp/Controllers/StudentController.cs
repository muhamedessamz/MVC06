using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.Data;
using UniversityApp.Models;
using UniversityApp.Models.ViewModels;
using System;

namespace UniversityApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // ================= SHOW ALL =================
        public async Task<IActionResult> ShowAll(int page = 1, string? search = null, string? searchName = null, int? departmentId = null, int pageSize = 5)
        {
            var searchValue = !string.IsNullOrEmpty(search) ? search : (searchName ?? string.Empty);

            var query = _context.Students
                .Include(s => s.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(s => s.Name.Contains(searchValue));
            }

            if (departmentId.HasValue && departmentId.Value > 0)
            {
                query = query.Where(s => s.DepartmentId == departmentId.Value);
            }

            int totalItems = await query.CountAsync();

            var students = await query
                .OrderBy(s => s.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age,
                    DepartmentId = s.DepartmentId,
                    DepartmentName = s.Department != null ? s.Department.Name : ""
                })
                .ToListAsync();

            var departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync();

            ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                departments, "Id", "Name", departmentId);

            var model = new StudentListViewModel
            {
                Students = students,
                Departments = departments,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Search = searchValue ?? string.Empty,
                DepartmentId = departmentId
            };

            model.SearchName = model.Search;

            return View(model);
        }

        // ================= DETAILS =================
        public async Task<IActionResult> ShowDetails(int id)
        {
            var student = await _context.Students
                .Include(s => s.Department)
                .Where(s => s.Id == id)
                .Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age,
                    DepartmentId = s.DepartmentId,
                    DepartmentName = s.Department != null ? s.Department.Name : ""
                })
                .FirstOrDefaultAsync();

            if (student == null)
                return NotFound();

            return View(student);
        }

        // ================= CREATE =================
        public async Task<IActionResult> Create()
        {
            var vm = new StudentFormViewModel
            {
                Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync();
                return View(model);
            }

            var student = new Student
            {
                Name = model.Name,
                Age = model.Age,
                DepartmentId = model.DepartmentId!.Value
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowAll));
        }

        // ================= EDIT =================
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            var vm = new StudentFormViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                DepartmentId = student.DepartmentId,
                Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync();
                return View(model);
            }

            var student = await _context.Students.FindAsync(model.Id);
            if (student == null)
                return NotFound();

            student.Name = model.Name;
            student.Age = model.Age;
            student.DepartmentId = model.DepartmentId!.Value;

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowAll));
        }

        // ================= DELETE =================
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return NotFound();

            var model = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                DepartmentId = student.DepartmentId,
                DepartmentName = student.Department?.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, StudentViewModel model)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowAll));
        }
    }
}
