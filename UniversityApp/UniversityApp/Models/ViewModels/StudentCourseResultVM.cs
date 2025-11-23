namespace UniversityApp.Models.ViewModels
{
    public class StudentCourseResultVM
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public int Grade { get; set; }
        public int MinDegree { get; set; }

        public bool IsPassed { get; set; }
        public string CssClass { get; set; }
    }
}
