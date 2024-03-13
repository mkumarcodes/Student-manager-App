using Assingment_2MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Assingment_2MVC.Data
{
    public class RegistrationContext : DbContext
    {
        public RegistrationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
        new Course { CourseId = 1, CourseName = "Course 1", CourseInstructor = "Instructor 1", StartDate = DateTime.Now, RoomNumber = "Room 101" }
        // Add more courses as needed
    );

            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, Name = "Student 1", Email = "student1@example.com",  CourseId = 1 }
                // Add more students as needed
            );
            modelBuilder.Entity<Student>()
                         .Property(Student => Student.StudentStatus)
                         .HasConversion<string>()
                         .HasMaxLength(64);

        }

    }
}

