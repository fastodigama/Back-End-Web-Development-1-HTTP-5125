namespace Cumulative1.Models
{
    //Teacher Class model is to represent the teacher data on the database
    public class Teacher
    {
        public int TeacherId { get; set; } // TeacherId will represent teacherid in the teachers table
        public string TeacherFname { get; set; } // TeacherFname will represent teacherfname in the teachers table
        public string TeacherLname { get; set; } // TeacherLname will represent teacherlname in the teachers table
        public string EmployeeNumber { get; set; } // EmployeeNumber will represent employee_number in the teachers table
        public DateTime HireDate { get; set; } // HireDate will represent hiredate in the teachers table
        public decimal Salary { get; set; } // Salary will represent salary in the teachers table.

        public List<Courses>? teacherCourses { get; set; }

        public string? errorMessage { get; set; } // to display error msg if teacher is not found

        public string teacherworkphone { get; set; } // TeacherWorkPhone will represent teacherworkphone in the teachers table
    }
}
