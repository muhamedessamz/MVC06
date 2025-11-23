using System.Collections.Generic;

namespace UniversityApp.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public string DepartmentName { get; set; }
        public List<string> StudentsAbove25 { get; set; }
        public string State { get; set; }
    }
}
