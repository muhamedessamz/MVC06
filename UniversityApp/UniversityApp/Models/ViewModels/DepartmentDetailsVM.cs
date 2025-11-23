namespace UniversityApp.ViewModels
{
    public class DepartmentDetailsVM
    {
        public string DepartmentName { get; set; } = string.Empty;
        public List<string> StudentNamesAbove25 { get; set; } = new List<string>();
        public string DepartmentState { get; set; } = string.Empty;
    }
}
