using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment_day06.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [StringLength(120)]
        public string FullName { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
