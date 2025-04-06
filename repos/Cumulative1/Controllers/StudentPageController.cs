using Cumulative1.Models; 
using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    
    public class StudentPageController : Controller
    {
        // Reference to the API controller for student 
        private StudentAPIController _APIcontroller;

        
        public StudentPageController(StudentAPIController APIcontroller)
        {
            _APIcontroller = APIcontroller;
        }

        // method to list all students
        public IActionResult StudentList()
        {
            
            List<Students> students = _APIcontroller.ListStudents();

            // Pass the student list to the view
            return View(students);
        }

        // Action method to show details of a specific student
        [HttpGet] // Specifies a GET request 
        public IActionResult StudentShow(int ID)
        {
            // Call the API controller to find the selected student by ID
            Students SelectedStudent = _APIcontroller.FindStudent(ID);

            // Pass the selected student to the view
            return View(SelectedStudent);
        }

        [HttpGet]

        public IActionResult NewStudent()
        {

            //redirect to the new student page
            return View();
        }

        /// <summary>
        /// Adds a new student to the database using the provided student information.
        /// After successfully adding the student, redirects the user to the StudentList view.
        /// </summary>
        /// <param name="NewStudent">
        /// A Students object containing the new student's details:
        /// - StudentFirstName: The first name of the student.
        /// - StudentLastName: The last name of the student.
        /// - StudentNumber: A unique identifier for the student.
        /// - StudentEnrollDate: The date the student enrolled.
        /// </param>
        /// <returns>
        /// An IActionResult that redirects to the StudentList view.
        /// </returns>
        /// <example>
        /// POST api/studentapi/createstudent
        /// Header: Content-Type: application/json
        /// Body:
        /// {
        ///   "StudentFirstName": "Alice",
        ///   "StudentLastName": "Brown",
        ///   "StudentNumber": "S123",
        ///   "StudentEnrollDate": "2025-04-05"
        /// }
        /// Example cURL:
        /// curl -X "POST" -d "{\"StudentFirstName\":\"Alice\",\"StudentLastName\":\"Brown\",\"StudentNumber\":\"N123\",\"StudentEnrollDate\":\"2025-04-05\"}" -H "Content-Type: application/json" "https://localhost:xx/API/StudentAPI/CreateStudent"
        /// </example>


        [HttpPost]
        public IActionResult CreateStudent(Students NewStudent)
        {
            _APIcontroller.AddStudent(NewStudent);

            return RedirectToAction("StudentList", NewStudent.StudentId);
        }


        // Action method to confirm delete a student
        [HttpGet]
        public IActionResult StudentDeleteConfirm(int Id)
        {
            //find the student
            Students SelectedStudent = _APIcontroller.FindStudent(Id);

            // Pass the selected student to the StudentDeleteConfirm view
            return View(SelectedStudent);
        }
        [HttpPost]

        
        public IActionResult Delete(int Id)
        {
            //send the student id to the api and delets it
            _APIcontroller.DeleteStudent(Id);

            return RedirectToAction("StudentList");
        }

    }
}
