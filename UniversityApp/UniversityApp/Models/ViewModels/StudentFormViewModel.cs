using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniversityApp.Models;

namespace UniversityApp.Models.ViewModels
{
    public class StudentFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Range(16, 100, ErrorMessage = "Age must be between 16 and 100")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int? DepartmentId { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
