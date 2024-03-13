using System.ComponentModel.DataAnnotations;

namespace Assingment_2MVC.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set;}

        public StudentStatus StudentStatus { get; set; }

        public int CourseId { get; set; }  // Add this property

        public Course? Course { get; set; }



    }

   public enum StudentStatus
    {
        ConfirmationMessageNotSent=0,
        ConfirmationMessageSent=1,
        EnrollmentConfirmed=2,
        EnrollmentDeclined=3
    }
}
