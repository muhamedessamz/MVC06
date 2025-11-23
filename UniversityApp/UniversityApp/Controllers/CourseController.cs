using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.Data;
using UniversityApp.Models;
using UniversityApp.Models.ViewModels;

namespace UniversityApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        // ================= INDEX =================
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.Department)
                .OrderBy(c => c.Id)
                .ToListAsync();

            return View(courses);
        }

        // ================= DETAILS =================
        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        // ================= CREATE (GET) =================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CourseFormViewModel
            {
                Departments = await _context.Departments
                    .OrderBy(d => d.Name)
                    .ToListAsync()
            };
            return View(vm);
        }

        // ================= CREATE (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departments = await _context.Departments.ToListAsync();
                return View(vm);
            }

            var course = new Course
            {
                Name = vm.Name,
                MinDegree = vm.MinDegree,
                DepartmentId = vm.DepartmentId  // ✅ هنا هو المفتاح الأجنبي بس
            };

            _context.Courses.Add(course);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Database error: " + ex.InnerException?.Message);
                vm.Departments = await _context.Departments.ToListAsync();
                return View(vm);
            }
        }

        // ================= EDIT (GET) =================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            var vm = new CourseFormViewModel
            {
                Id = course.Id,
                Name = course.Name,
                MinDegree = course.MinDegree,
                DepartmentId = course.DepartmentId,
                Departments = await _context.Departments.OrderBy(d => d.Name).ToListAsync()
            };

            return View(vm);
        }

        // ================= EDIT (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Departments = await _context.Departments.ToListAsync();
                return View(vm);
            }

            var course = await _context.Courses.FindAsync(vm.Id);
            if (course == null)
                return NotFound();

            course.Name = vm.Name;
            course.MinDegree = vm.MinDegree;
            course.DepartmentId = vm.DepartmentId;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ================= DELETE (GET) =================
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        // ================= DELETE (POST) =================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ================= STUDENT COURSE RESULT =================
        public async Task<IActionResult> StudentCourseResult(int studentId, int courseId)
        {
            var record = await _context.StuCrsRes
                .Include(x => x.Student)
                .Include(x => x.Course)
                .FirstOrDefaultAsync(x => x.StudentId == studentId && x.CourseId == courseId);

            if (record == null)
                return NotFound();

            var vm = new StudentCourseResultVM
            {
                StudentId = record.StudentId,
                StudentName = record.Student.Name,
                CourseName = record.Course.Name,
                Grade = record.Grade,
                MinDegree = record.Course.MinDegree,
                IsPassed = record.Grade >= record.Course.MinDegree,
                CssClass = record.Grade >= record.Course.MinDegree ? "text-success" : "text-danger"
            };

            return View(vm);
        }
    }
}
