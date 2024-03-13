namespace Assingment_2MVC.Models
{
    public class ManageCourseViewModel
    {
        public Course? Course { get; set; }

        public Student? Student { get; set; }

        public List<Student>? Students { get; set; }


        public int CountConfirmationMessageNotSent { get; set; }

        public int CountConfirmationMessageSent { get; set; }

        public int CountEnrollmentConfirmed { get; set; }

        public int CountEnrollmentDeclined { get; set; }
    }
}
