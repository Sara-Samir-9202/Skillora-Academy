using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace StudentSystem.Models
{
    public class Instructor
    {
        [Key]
        public int InsId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;

        [Range(22, 60, ErrorMessage = "Age must be between 22 and 60")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(5000, 15000, ErrorMessage = "Salary must be between 5000 and 15000")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        public string Degree { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Remote(action: "ValidateEmail", controller: "Instructor",
                AdditionalFields = "InsId",
                ErrorMessage = "This email already exists")]
        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        public int DeptId { get; set; }

        [ForeignKey(nameof(DeptId))]
        public Department Department { get; set; } = null!;

        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
            = new List<CourseInstructor>();
    }
}