using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityApp.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "MinDegree must be between 0 and 100")]
        public int MinDegree { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; } 

        public Department? Department { get; set; }  

        public ICollection<StuCrsRes> StuCrsRes { get; set; } = new List<StuCrsRes>();
    }
}
