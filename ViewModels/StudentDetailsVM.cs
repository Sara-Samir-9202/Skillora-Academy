namespace StudentSystem.ViewModels
{
    public class StudentDetailsVM
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public string? DepartmentName { get; set; }

        public List<CourseDegreeVM>? Courses { get; set; }
    }
}