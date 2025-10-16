using System.ComponentModel.DataAnnotations;

namespace Assignment_day06.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(1, 10)]
        public int Credits { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
