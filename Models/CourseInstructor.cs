namespace StudentSystem.Models
{
    public class CourseInstructor
    {
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;

        public int InstructorId { get; set; }
        public virtual Instructor Instructor { get; set; } = null!;
    }
}