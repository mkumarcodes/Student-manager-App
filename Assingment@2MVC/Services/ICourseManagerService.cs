using Assingment_2MVC.Models;

namespace Assingment_2MVC.Services
{
    public interface ICourseManagerService
    {
        List<Course>? GetAllCources();

        public List<Student>? GetAllStudents(int courseId);


        public Course? GetCourseById(int id);

        public int AddCourse(Course Sub);

        public int AddStudent(Student student);

        public int UpdateCourse(Course Sub);

        public Student? GetStudentById(int courseId, int studentId);

        public void UpdateConfirmationStatus(int courseId, int studentId, StudentStatus status);

        public void SentEnrollEmailWithCourseID(int courseId, string scheme, string host);
    }
}
