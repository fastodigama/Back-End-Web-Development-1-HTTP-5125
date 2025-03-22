using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Cumulative1.Models;

namespace Cumulative1.Controllers
{
    // API Controller to manage Courses
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesAPIController : ControllerBase
    {
        private readonly SchoolDbContext _connection;

        public CoursesAPIController(SchoolDbContext connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// This Method will return Courses in the Database
        /// </summary>
        /// <example>
        /// CURL -X "GET" "https://localhost:XX/api/coursesapi/listcourses"
        /// </example>
        /// <returns>A list of course objects</returns>
        [HttpGet("ListCourses")]
        public List<Courses> ListCourses()
        {
            using (MySqlConnection Connection = _connection.AccessDatabase())
            {
                List<Courses> CourseList = new List<Courses>();

                Connection.Open(); // Open database connection
                MySqlCommand command = Connection.CreateCommand(); // Create SQL command
                string sqlQuery = "SELECT * FROM courses"; // Query to retrieve all course info
                command.CommandText = sqlQuery;

                // Execute query and process results
                using (MySqlDataReader Reader = command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        // Populate course data
                        int CourseID = Convert.ToInt32(Reader["courseid"]);
                        string CourseCode = Reader["coursecode"].ToString();
                        string CourseName = Reader["coursename"].ToString();
                        int TeacherID = Convert.ToInt32(Reader["teacherid"]);
                        DateTime StartDate = Convert.ToDateTime(Reader["startdate"]);
                        DateTime EndDate = Convert.ToDateTime(Reader["finishdate"]);

                        // Create course object and add it to the list
                        Courses CurrentCourse = new Courses();
                       

                        // declare a Student object to use it to add to the Student List

                        
                        CurrentCourse.courseId = CourseID;
                        CurrentCourse.courseCode = CourseCode;
                        CurrentCourse.courseName = CourseName;
                        CurrentCourse.teacherId = TeacherID;
                        CurrentCourse.startDate = StartDate;
                        CurrentCourse.endDate = EndDate;

                        CourseList.Add(CurrentCourse);
                    }
                }

                return CourseList;
            }
        }

        /// <summary>
        /// Finds a course by its ID
        /// </summary>
        /// <example>
        /// CURL -X "GET" "https://localhost:XX/api/coursesapi/findcourse/1"
        /// </example>
        /// <returns>A course object</returns>
        [HttpGet("FindCourse/{CourseId}")]
        public Courses FindCourse(int CourseId)
        {
            Courses SelectedCourse = new Courses();

            using (MySqlConnection Connection = _connection.AccessDatabase())
            {
                Connection.Open(); // Open database connection
                MySqlCommand command = Connection.CreateCommand();
                string query = "SELECT CONCAT (t.teacherfname,' ', t.teacherlname) AS TeacherName , c.* FROM courses c INNER JOIN teachers t ON c.teacherid = t.teacherid WHERE courseid = @CourseId;"; 
                command.CommandText = query;

                // Add parameter to prevent SQL injection
                command.Parameters.AddWithValue("@CourseId", CourseId);

                using (MySqlDataReader ResultSet = command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        // Populate course details
                        SelectedCourse.courseId = Convert.ToInt32(ResultSet["courseid"]);
                        SelectedCourse.courseCode = ResultSet["coursecode"].ToString();
                        SelectedCourse.courseName = ResultSet["coursename"].ToString();
                        SelectedCourse.teacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        SelectedCourse.startDate = Convert.ToDateTime(ResultSet["startdate"]);
                        SelectedCourse.endDate = Convert.ToDateTime(ResultSet["finishdate"]);
                        SelectedCourse.TeacherName = ResultSet["TeacherName"].ToString();
                    }
                }
            }

            return SelectedCourse;
        }
    }
}
