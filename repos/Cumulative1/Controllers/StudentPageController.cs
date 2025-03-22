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
    }
}
