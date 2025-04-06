namespace Cumulative1.Models
{
    // The Courses class represents a course entity in the system.
    public class Courses
    {
        // Unique identifier for the course.
        public int courseId { get; set; }

        // Code associated with the course
        public string courseCode { get; set; }

        // Name of the course 
        public string courseName { get; set; }

        // The ID of the teacher associated with this course.
        public int teacherId { set; get; }

        // The start date of the course.
        public DateTime startDate { set; get; }

        // The end date of the course.
        public DateTime endDate { set; get; }

        public string? TeacherName { get; set; }
    }
}
