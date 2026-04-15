using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSystem.Models
{
    public class Student
    {
        [Key]
        public int SSN { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters")]
        [MaxLength(15, ErrorMessage = "Name must be at most 15 characters")]
        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }

        public string? Image { get; set; }

        [Range(12, 24, ErrorMessage = "Age must be between 12 and 24")]
        public int? Age { get; set; }

        [ForeignKey(nameof(Department))]
        public int? DeptId { get; set; }

        public virtual Department? Department { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
            = new List<StudentCourse>();
    }
}