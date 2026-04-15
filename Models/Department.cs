using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
            = new List<Student>();
    }
}