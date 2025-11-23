using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniversityApp.Models;

namespace UniversityApp.Models.ViewModels
{
    public class CourseFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Minimum degree is required")]
        [Range(0, 100, ErrorMessage = "MinDegree must be between 0 and 100")]
        public int MinDegree { get; set; }

        [Required(ErrorMessage = "Please select a department")]
        public int DepartmentId { get; set; }

        public List<Department> Departments { get; set; } = new();
    }
}
