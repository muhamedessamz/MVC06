using System.ComponentModel.DataAnnotations;

namespace UniversityApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Range(18, 50, ErrorMessage = "Age must be between 18 and 50")]
        public int Age { get; set; }

        [Required(ErrorMessage = "You must select a Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<StuCrsRes> StuCrsRes { get; set; } = new List<StuCrsRes>();
    }
}
