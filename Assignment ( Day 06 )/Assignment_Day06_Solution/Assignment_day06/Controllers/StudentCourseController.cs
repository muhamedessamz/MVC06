using Assignment_day06.Data;
using Assignment_day06.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentCourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /StudentCourse/GetStudentCourse?studentId=1&courseId=2
        public async Task<IActionResult> GetStudentCourse(int studentId, int courseId)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrollment == null) return NotFound();

            // لا نضع شروط في الـ View — القرار (pass/fail) هنا في الكونترولر:
            bool passed = enrollment.Degree >= 50; // مثال: 50 نقطة نجاح
            var vm = new StudentCourseVM
            {
                StudentName = enrollment.Student.FullName,
                CourseName = enrollment.Course.Name,
                Degree = enrollment.Degree,
                Color = passed ? "green" : "red"
            };

            return View(vm);
        }
    }
}
