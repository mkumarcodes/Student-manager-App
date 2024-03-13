using System.ComponentModel.DataAnnotations;

namespace Assingment_2MVC.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string CourseInstructor { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        [RegularExpression(@"^[0-9][A-Z]\d{2}$", ErrorMessage = "Room number must be in the format: 3G15")]
        public string RoomNumber { get; set; }

        public List<Student>? Students { get; set; }
    }
}
