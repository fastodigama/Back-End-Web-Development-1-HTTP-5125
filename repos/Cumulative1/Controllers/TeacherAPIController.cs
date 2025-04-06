using System.Data.Common;
using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using Mysqlx.Datatypes;
using System.Text.RegularExpressions;


namespace Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _connection;

        public TeacherAPIController(SchoolDbContext connection)
        {
            _connection = connection;
        }
        /// <summary>
        /// Retrieves a list of all teachers from the database.
        /// CURL -X "GET" "https://localhost:XX/api/teacherapi/ListTeachers"
        /// </summary>
        /// 
        /// <returns>
        /// A list of Teacher objects, representing all teachers in the database.
        /// 
        /// [
        ///   {
        ///     [{"teatcherId":1,"teacherFname":"Alexander",
        ///     "teacherLname":"Bennett",
        ///     "employeeNumber":"T378",
        ///     "hireDate":"2016-08-05T00:00:00","salary":55.30}, ,{"teatcherId":2,
        ///     "teacherFname":"Caitlin",
        ///     "teacherLname":"Cummings",
        ///     "employeeNumber":"T381","hireDate":"2014-06-10T00:00:00","salary":62.77}
        ///   }
        /// ]
        /// </returns>  
        [HttpGet(template: "ListTeachers")]

        public List<Teacher> ListTeachers(DateTime fromDate = default , DateTime toDate = default)
        {
            List<Teacher> Teachers = new List<Teacher>(); //define a list to add the list
                                                          //of retrieved teachers from the database in it

            using (MySqlConnection Connection = _connection.AccessDatabase())
            {
                Connection.Open(); //open the connection to the database
                MySqlCommand command = Connection.CreateCommand(); //define the sql command
                string sqlQuery = "SELECT * FROM teachers"; //sql query to retrieve all teachers info
                
                // Check if both fromDate and toDate parameters have been provided(not default)
                if (fromDate != default && toDate != default)
                {
                    // Add a WHERE clause to the SQL query to filter records based on the hire date range
                    sqlQuery += " WHERE hiredate >= @fromDate AND hiredate <= @toDate";

                    //
                    command.Parameters.AddWithValue("@fromDate", fromDate);// Binds the fromDate parameter to @fromDate in the query
                    command.Parameters.AddWithValue("@toDate", toDate); // Binds the toDate parameter to @toDate in the query
                }

                

                command.CommandText = sqlQuery; //assign the query to the command

                command.Prepare(); // Prepares the SQL command before execute reader

                //execute the command through sql reader

                using (MySqlDataReader Reader = command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        //add teacher information to the teachers List
                        //first we need to define variables to hold the teacher info from the Reader

                        int TeacherID = Convert.ToInt32(Reader["teacherid"]);
                        string TeacherFirstName = Reader["teacherfname"].ToString();
                        string TeacherLastName = Reader["teacherlname"].ToString();
                        string EmployeeNumber = Reader["employeenumber"].ToString();
                        DateTime HireDate = Convert.ToDateTime(Reader["hiredate"]);
                        decimal Salary = Convert.ToDecimal(Reader["salary"]);

                        // declare a Teacher object to use it to add to the Teacher List

                        Teacher CurrentTeacher = new Teacher();
                        CurrentTeacher.TeatcherId = TeacherID;
                        CurrentTeacher.TeacherFname = TeacherFirstName;
                        CurrentTeacher.TeacherLname = TeacherLastName;
                        CurrentTeacher.EmployeeNumber = EmployeeNumber;
                        CurrentTeacher.HireDate = HireDate;
                        CurrentTeacher.Salary = Salary;

                        Teachers.Add(CurrentTeacher);
                    }
                }
                
                return Teachers;

            }





        }
        /// <summary>
        /// Retrieves a single teacher's information from the database based on the provided TeacherId.

        /// </summary>
        /// 
        /// <param name="TeacherId"></param>
        /// CURL -X "GET" "https://localhost:XX/api/teacherapi/findteacher/8"
        /// <returns>
        /// A single teacher information that matches the supplied teacherId
        /// {"teatcherId":8,"teacherFname":"Dana","teacherLname":"Ford",.
        /// "employeeNumber":"T401","hireDate":"2014-06-26T00:00:00","salary":71.15}
        /// 
        /// </returns>

        [HttpGet(template: "FindTeacher/{TeacherId}")]

        public Teacher FindTeacher(int TeacherId)
        {
                
            //declare a Teacher variable to use it to store retrieved teacher data
            Teacher SelectedTeacher = new Teacher();
            

            //error out of scoop handiling
            if (TeacherId <= 0)
            {
                return new Teacher { errorMessage = "Invalid TeacherId. TeacherId must be a positive integer." };
            }

            using (MySqlConnection Connection = _connection.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand command = Connection.CreateCommand();

                //LEFT JOIN To Include teachers with no courses
                string sqlQuery = "SELECT t.teacherworkphone, t.employeenumber , t.hiredate, t.salary, t.teacherfname," +
                    " t.teacherlname, t.teacherid," +
                    " c.coursecode, c.courseid, c.coursename,c.finishdate,c.startdate" +
                    " FROM teachers t LEFT JOIN courses c ON t.teacherid = c.teacherid WHERE t.teacherid=@TeacherId;";
                command.Parameters.AddWithValue("@TeacherId", TeacherId);
                command.CommandText = sqlQuery;

                using (MySqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {



                        SelectedTeacher.TeatcherId = Convert.ToInt32(dataReader["teacherid"]);
                        SelectedTeacher.TeacherFname = dataReader["teacherfname"].ToString();
                        SelectedTeacher.TeacherLname = dataReader["teacherlname"].ToString();
                        SelectedTeacher.EmployeeNumber = dataReader["employeenumber"].ToString();
                        SelectedTeacher.HireDate = Convert.ToDateTime(dataReader["hiredate"]);
                        SelectedTeacher.Salary = Convert.ToDecimal(dataReader["salary"]);
                        SelectedTeacher.teacherworkphone = dataReader["teacherworkphone"].ToString();
                        SelectedTeacher.teacherCourses = new List<Courses>();

                        // Check if course fields are NULL
                        if (dataReader["courseid"] != DBNull.Value)

                            {
                                Courses CourseInfo = new Courses()
                            {
                                courseCode = dataReader["coursecode"].ToString(),
                                courseId = Convert.ToInt32(dataReader["courseid"]),
                                courseName = dataReader["coursename"].ToString(),
                                startDate = Convert.ToDateTime(dataReader["startdate"]),
                                endDate = Convert.ToDateTime(dataReader["finishdate"])

                            };

                            SelectedTeacher.teacherCourses.Add(CourseInfo);





                        }

                    }

                }
                return SelectedTeacher;

            }

        }



        /// <summary>
        /// Adds a new teacher to the database with the provided teacher information.
        /// Validates the input data to ensure correctness and avoids duplicate entries.
        /// </summary>
        /// <param name="NewTeacher">
        /// A Teacher object containing details such as first name, last name, employee number, hire date, and salary.
        /// </param>
        /// <returns>
        /// A confirmation message indicating success or failure:
        /// - On success, returns the ID of the newly added teacher.
        /// - On failure, returns a message describing the validation error or the issue (e.g., duplicate employee number or no name provided or future hire date).
        /// </returns>
        /// <example>
        /// POST api/teacherapi/addteacher
        /// Header: Content-Type: application/json
        /// Body: 
        /// {
        ///   "teacherFname": "Fadel",
        ///   "teacherLname": "I M",
        ///   "employeeNumber": "T123",
        ///   "hireDate": "1981-05-25",
        ///   "salary": 99.3
        ///   "teacherworkphone":"4168051041"
        /// }
        /// Example cURL:
        /// curl -X "POST" -d "{\"TeacherFname\":\"Fadel\",\"TeacherLname\":\"I M\",\"EmployeeNumber\":\"T123\",\"HireDate\":\"1981-05-25\",\"Salary\":\"99.3\",\"teacherworkphone\":\"4168051041\"}" -H "Content-Type: application/json" "https://localhost:xx/API/TeacherAPI/AddTeacher"
        /// </example>

        [HttpPost(template: "AddTeacher")]


        public object AddTeacher([FromBody] Teacher NewTeacher)
        {

            // check if teacher first or last name is null or empty

            if (string.IsNullOrEmpty(NewTeacher.TeacherFname) || string.IsNullOrEmpty(NewTeacher.TeacherLname))
            {
                return "Teacher first name or last name cannot be empty.";

            }

            // check if Teacher Hire Date is in the future

            if (NewTeacher.HireDate > DateTime.Now)
            {
                return "Teacher hire date cannot be in the future.";
            }

            //check Employee Number is not “T” followed by digits

            //source:https://www.youtube.com/watch?v=0lU_DG7s0qA&t=20s

            var pattern = @"^T\d{3}$";

            Match employeeNumberMatch = Regex.Match(NewTeacher.EmployeeNumber, pattern);

            if (!employeeNumberMatch.Success)
            {
                return "Employee number must start with 'T' followed by digits.";
            }

            //adding the teacher
              string query = "INSERT INTO teachers(teacherfname, teacherlname, employeenumber, hiredate, salary,teacherworkphone) " +
                "VALUES(@teacherfname, @teacherlname,@employeenumber,@hiredate,@salary,@teacherworkphone);";

            using (MySqlConnection Connection = _connection.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand command = Connection.CreateCommand();

                //check if the employee number already exists in the database
                string selectQuery = "SELECT * FROM teachers WHERE employeenumber=@eid";
                command.Parameters.AddWithValue("@eid", NewTeacher.EmployeeNumber);

                command.CommandText = selectQuery;
                using (MySqlDataReader Reader = command.ExecuteReader())
                {
                    if (Reader.Read())
                    {                        
                            return "Employee number already exists. Please use a different employee number.";
                        
                    }
                }

                    command.CommandText = query;
                    command.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFname);
                    command.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLname);
                    command.Parameters.AddWithValue("@employeenumber", NewTeacher.EmployeeNumber);
                    command.Parameters.AddWithValue("@hiredate", NewTeacher.HireDate);
                    command.Parameters.AddWithValue("@salary", NewTeacher.Salary);
                //initiative
                    command.Parameters.AddWithValue("@teacherworkphone", NewTeacher.teacherworkphone);
                    command.Prepare();
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(command.LastInsertedId);
                }
           
        }
        


        /// <summary>
        /// This method will recieve the teacher id  from the DeleteConfirm view 
        /// and delete a teacher from the database 
        /// </summary>
        /// <param name="TeacherId"></param> // The primary key of the teacher to be deleted
        /// <returns>
        /// Confirmation message indicating the teacher has been deleted
        /// </returns>
        /// <example>
        ///  DELETE api/TeacherApi/DeleteTeacher/12
        /// 

        [HttpDelete(template: "DeleteTeacher/{TeacherId}")]

        public string DeleteTeacher(int TeacherId)
        {
            if (TeacherId <= 0)
            {
                return "Invalid TeacherId. TeacherId must be a positive integer.";
            }
            using (MySqlConnection Connection = _connection.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand command = Connection.CreateCommand();
                string sqlQuery = "DELETE FROM teachers WHERE teacherid = @TeacherId";
                command.Parameters.AddWithValue("@TeacherId", TeacherId);
                command.CommandText = sqlQuery;
                int numberOfAffectedRows = command.ExecuteNonQuery();

                //Error Handling on Delete when trying to delete a teacher that does not exist

                if (numberOfAffectedRows == 0)
                {
                    return "No teacher found with ID " + TeacherId + ".";
                }

            

            return "Teacher with ID " + TeacherId + " has been deleted.";
            }
        }









    }
}

