using System.Diagnostics;
using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace Cumulative1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {

        private readonly SchoolDbContext _connection;

        public StudentAPIController(SchoolDbContext connection)
        {
            _connection = connection;
        }



        /// <summary>
        /// This Method will return Students in the Database
        /// </summary>
        /// <example>
        /// CURL -X "GET" "https://localhost:XX/api/studentapi/liststudents"
        /// 
        /// [{"studentId":1,"studentFirstName":"Sarah","studentLastName":"Valdez","studentNumber":"N1678",
        /// "studentEnrollDate":"2018-06-18T00:00:00"},{"studentId":2,"studentFirstName":"Jennifer",
        /// "studentLastName":"Faulkner",
        /// "studentNumber":"N1679","studentEnrollDate":"2018-08-02T00:00:00"}]
        /// </example>
        /// <returns>a list of students objects</returns>
        /// 
        [HttpGet(template: "ListStudents")]
        public List<Students> ListStudents()
        {


            using (MySqlConnection Connection = _connection.AccessDatabase())
            {

                List<Students> Students = new List<Students>();

                Connection.Open(); //open the connection to the database
                MySqlCommand command = Connection.CreateCommand(); //define the sql command
                string sqlQuery = "SELECT * FROM students"; //sql query to retrieve all students info

                command.CommandText = sqlQuery; //assign the query to the command

                //execute the command through sql reader

                using (MySqlDataReader Reader = command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        //add student information to the student List
                        //first we need to define variables to hold the student info from the Reader

                        int StudentID = Convert.ToInt32(Reader["studentid"]);
                        string StudentFirstName = Reader["studentfname"].ToString();
                        string StudentLastName = Reader["studentlname"].ToString();
                        string StudentNumber = Reader["Studentnumber"].ToString();
                        DateTime EnrollDate = Convert.ToDateTime(Reader["enroldate"]);


                        // declare a Student object to use it to add to the Student List

                        Students CurrentStudent = new Students();
                        CurrentStudent.StudentId = StudentID;
                        CurrentStudent.StudentFName = StudentFirstName;
                        CurrentStudent.StudentLName = StudentLastName;
                        CurrentStudent.studentnumber = StudentNumber;
                        CurrentStudent.StudentEnrolDate = EnrollDate;


                        Students.Add(CurrentStudent);
                    }
                }

                return Students;

            }
        }
            [HttpGet(template: "FindStudent/{StudentId}")]

             public Students FindStudent(int StudentId)
            {
                Students SelectedStudent = new Students();

                using (MySqlConnection Connection = _connection.AccessDatabase())
                {
                    Connection.Open();
                    MySqlCommand command = Connection.CreateCommand();
                    string query = "SELECT * FROM students WHERE studentid =" + StudentId;
                    command.CommandText = query;
                    using (MySqlDataReader ResultSet = command.ExecuteReader())
                    {
                        while (ResultSet.Read())
                        {
                            SelectedStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                            SelectedStudent.studentnumber = ResultSet["studentnumber"].ToString();
                            SelectedStudent.StudentFName = ResultSet["studentfname"].ToString();
                            SelectedStudent.StudentLName = ResultSet["studentlname"].ToString();
                            SelectedStudent.StudentEnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

                        }
                    }
                }


                return SelectedStudent;
            }

        /// <summary>
        /// Adds a new student record to the database using the provided student information.
        /// This method validates and inserts the details, returning the ID of the newly added student.
        /// </summary>
        /// <param name="NewStudent">
        /// A Students object containing the following properties:
        /// - StudentFirstName: The first name of the student.
        /// - StudentLastName: The last name of the student.
        /// - StudentNumber: A unique identifier for the student.
        /// - StudentEnrollDate: The date the student enrolled.
        /// </param>
        /// <returns>
        /// The ID of the newly inserted student as an integer.
        /// </returns>
        /// <example>
        /// POST api/studentapi/addstudent
        /// Header: Content-Type: application/json
        /// Body: 
        /// {
        ///   "StudentFirstName": "John",
        ///   "StudentLastName": "Doe",
        ///   "StudentNumber": "S456",
        ///   "StudentEnrollDate": "2023-09-01"
        /// }
        /// Example cURL:
        /// curl -X "POST" -d "{\"StudentFirstName\":\"John\",\"StudentLastName\":\"Doe\",.\"StudentNumber\":\"S456\",\"StudentEnrollDate\":\"2023-09-01\"}" -H "Content-Type: application/json" "https://localhost:xx/API/StudentAPI/AddStudent"    
        /// </example>

        [HttpPost(template: "AddStudent")]

        public int AddStudent([FromBody] Students NewStudent)
        {
            using (MySqlConnection Connection = _connection.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand command = Connection.CreateCommand();
                string query = "INSERT INTO students(studentfname, studentlname, studentnumber, enroldate)" +
                    " VALUES(@studentfname, @studentlname, @studentnumber, @enroldate);";

                Console.WriteLine(NewStudent.StudentEnrolDate);


                command.Parameters.AddWithValue("@studentfname", NewStudent.StudentFName);
                command.Parameters.AddWithValue("@studentlname", NewStudent.StudentLName);
                command.Parameters.AddWithValue("@studentnumber", NewStudent.studentnumber);
                command.Parameters.AddWithValue("@enroldate", NewStudent.StudentEnrolDate);
                Debug.WriteLine(NewStudent.StudentEnrolDate);
                command.CommandText = query;
                command.Prepare(); // Prepare the command to prevent SQL injection

                command.ExecuteNonQuery();

                //return last inserted id
                return Convert.ToInt32(command.LastInsertedId);

            }
        }

        /// <summary>
        /// Deletes a student record from the database based on the provided student ID.
        /// Validates the deletion by checking the number of affected rows and returns 
        /// a confirmation message indicating the success or failure of the operation.
        /// </summary>
        /// <param name="StudentId">
        /// The ID of the student to be deleted. This corresponds to the primary key in the students table.
        /// </param>
        /// <returns>
        /// A string message:
        /// - "Student Deleted Successfully" if the student was found and deleted.
        /// - "Student Not Found" if no student exists with the given ID.
        /// </returns>
        /// <example>
        /// DELETE api/studentapi/deletestudent/5
        /// This API call will delete the student with the ID 5.
        /// Example cURL:
        /// curl -X "DELETE" "https://localhost:xx/API/StudentAPI/DeleteStudent/5"
        /// </example>
        
        [HttpDelete(template: "/DeleteStudent/{StudentId}")]

        public string DeleteStudent(int StudentId)
        {
            using(MySqlConnection Connection = _connection.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand command = Connection.CreateCommand();
                string deleteQuery = "DELETE FROM students WHERE studentid = @studentid";
                command.CommandText = deleteQuery;
                command.Parameters.AddWithValue("@studentid", StudentId);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return "Student Deleted Successfully";
                }
                else
                {
                    return "Student Not Found";
                }
            }
        }
    }
}

