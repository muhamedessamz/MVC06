namespace UniversityApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? MgrName { get; set; } = string.Empty;

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
