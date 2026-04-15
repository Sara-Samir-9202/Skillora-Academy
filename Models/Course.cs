using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace StudentSystem.Models
{
    public class Course : IValidatableObject
    {
        [Key]
        public int CrsId { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Range(0, 100, ErrorMessage = "Min Degree must be between 0 and 100")]
        [Remote(action: "ValidateDegrees", controller: "Course",
                AdditionalFields = "MaxDegree",
                ErrorMessage = "Min Degree must be less than Max Degree")]
        public int? MinDegree { get; set; }

        [Range(0, 100, ErrorMessage = "Max Degree must be between 0 and 100")]
        public int? MaxDegree { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
            = new List<StudentCourse>();

        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
            = new List<CourseInstructor>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinDegree >= MaxDegree)
            {
                yield return new ValidationResult(
                    "MinDegree must be less than MaxDegree",
                    new[] { nameof(MinDegree), nameof(MaxDegree) }
                );
            }
        }
    }
}