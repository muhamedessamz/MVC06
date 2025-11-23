using System.Collections.Generic;
using UniversityApp.Models;

namespace UniversityApp.Models.ViewModels
{
    public class StudentListViewModel
    {
        public List<StudentViewModel> Students { get; set; } = new List<StudentViewModel>();
        public List<Department> Departments { get; set; } = new List<Department>();

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public string Search { get; set; } = "";
        public string SearchName { get { return Search; } set { Search = value; } }

        public int? DepartmentId { get; set; }
    }
}
