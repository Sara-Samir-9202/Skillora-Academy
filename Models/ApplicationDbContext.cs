using Microsoft.EntityFrameworkCore;

namespace StudentSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Instructor> Instructors { get; set; } = null!;
        public DbSet<StudentCourse> StudentCourses { get; set; } = null!;
        public DbSet<CourseInstructor> CourseInstructors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ================= StudentCourse (Composite Key) =================
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // ================= CourseInstructor (Composite Key) =================
            modelBuilder.Entity<CourseInstructor>()
                .HasKey(ci => new { ci.CourseId, ci.InstructorId });

            modelBuilder.Entity<CourseInstructor>()
                .HasOne(ci => ci.Course)
                .WithMany(c => c.CourseInstructors)
                .HasForeignKey(ci => ci.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseInstructor>()
                .HasOne(ci => ci.Instructor)
                .WithMany(i => i.CourseInstructors)
                .HasForeignKey(ci => ci.InstructorId)
                .OnDelete(DeleteBehavior.Cascade);

            // ================= Course Name UNIQUE =================
            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // ================= Optional constraints =================
            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<Course>()
                .Property(c => c.Description)
                .IsRequired();
        }
    }
}