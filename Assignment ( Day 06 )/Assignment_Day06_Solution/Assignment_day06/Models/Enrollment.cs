using Assignment_day06.Models;
using System.ComponentModel.DataAnnotations;

namespace Assignment_day06.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Range(0, 100)]
        public double Degree { get; set; }
    }
}
