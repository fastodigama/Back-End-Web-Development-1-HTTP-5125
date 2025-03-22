using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

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
                        CurrentStudent.StudentFirstName = StudentFirstName;
                        CurrentStudent.StudentLastName = StudentLastName;
                        CurrentStudent.StudentNumber = StudentNumber;
                        CurrentStudent.StudentEnrollDate = EnrollDate;


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
                            SelectedStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                            SelectedStudent.StudentFirstName = ResultSet["studentfname"].ToString();
                            SelectedStudent.StudentLastName = ResultSet["studentlname"].ToString();
                            SelectedStudent.StudentEnrollDate = Convert.ToDateTime(ResultSet["enroldate"]);

                        }
                    }
                }


                return SelectedStudent;
            }


        }
    }

