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
                string query = "SELECT CONCAT (t.teacherfname,' ', t.teacherlname) AS TeacherName , c.* FROM courses c " +
                    "INNER JOIN teachers t ON c.teacherid = t.teacherid WHERE courseid = @CourseId;"; 
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
        /// <summary>
        /// Adds a new course record to the database.
        /// </summary>
        /// <param name="NewCourse">Courses object containing the course details to be added.</param>
        /// <example>
        /// POST: api/CourseAPI/AddCourse
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// {
        ///     "courseCode": "CSE101",
        ///     "teacherId": 12,
        ///     "startDate": "2025-01-10",
        ///     "endDate": "2025-05-15",
        ///     "courseName": "Introduction to Computer Science"
        /// }
        /// </example>
        /// <returns>
        /// The newly inserted CourseId from the database if successful.
        /// </returns>
        [HttpPost("AddCourse")]

        public int AddCourse([FromBody] Courses NewCourse)
        {
            using(MySqlConnection Connection = _connection.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand command = Connection.CreateCommand();

                string query = "INSERT INTO courses (courseid,coursecode,teacherid,startdate,finishdate,coursename) " +
                    "VALUES (@courseid, @coursecode,@teacherid,@startdate,@finishdate,@coursename);";

                //since auto_increment is not enabled on course ID, I will check the max ID in the database and use it

                string checkIdQuery = "SELECT MAX(courseid) AS maxID FROM courses";
                
                command.CommandText = checkIdQuery;
                int maxCourseId = 0;
                
                using(MySqlDataReader Reader = command.ExecuteReader())
                {
                    if (Reader.Read())
                    {
                        maxCourseId = Convert.ToInt32(Reader["maxID"]);
                    }
                }


                command.Parameters.AddWithValue("@courseid", maxCourseId + 1);
                command.Parameters.AddWithValue("@coursecode", NewCourse.courseCode);
                command.Parameters.AddWithValue("@teacherid", NewCourse.teacherId);
                command.Parameters.AddWithValue("@startdate", NewCourse.startDate);
                command.Parameters.AddWithValue("@finishdate", NewCourse.endDate);
                command.Parameters.AddWithValue("@coursename", NewCourse.courseName);
                command.CommandText = query;
                command.Prepare();
                command.ExecuteNonQuery();
                return maxCourseId;


            }
            
            
        }
        /// <summary>
        /// Deletes a course from the database using the specified CourseId.
        /// </summary>
        /// <param name="Id">The ID of the course to be deleted.</param>
        /// <example>
        /// DELETE: api/CourseAPI/DeleteCourse/123
        /// Response: "course with ID 123 was deleted forever."
        /// OR
        /// Response: "No course found with ID 123."
        /// </example>
        /// <returns>
        /// A message indicating whether the course was successfully deleted or if no course was found with the given ID.
        /// </returns>

        [HttpDelete(template:"DeleteCourse/{Id}")]

        public object DeleteCourse(int Id)
        {
            using(MySqlConnection conn = _connection.AccessDatabase())
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                string query = "DELETE FROM courses WHERE courseid=@Id";
                command.Parameters.AddWithValue("@Id", Id);
                command.CommandText = query;
                command.Prepare();
                
                int numberOfAffectedRows = command.ExecuteNonQuery();

                //Error Handling on Delete when trying to delete a course that does not exist

                if (numberOfAffectedRows == 0)
                {
                    return "No course found with ID " + Id + ".";
                }

                return "course with ID " + Id + " was deleted forever.";
            }

           
        }


    }

    
}
