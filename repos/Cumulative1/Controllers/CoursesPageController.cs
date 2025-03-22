using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class CoursesPageController : Controller
    {



        // Reference to the API controller for courses
        private CoursesAPIController _APIcontroller;

                public CoursesPageController(CoursesAPIController APIcontroller)
        {
            _APIcontroller = APIcontroller;
        }

        [HttpGet]
        public IActionResult CoursesList()
        {

            // Call the API controller to get the list of courses
            List<Courses> courses = _APIcontroller.ListCourses();

            // Pass the courses list to the view
            return View(courses);
        
            
        }

        [HttpGet]
        public IActionResult CourseShow(int ID)
        {
            // Call the API controller to find the  Course by ID
            Courses SelectedCourse = _APIcontroller.FindCourse(ID);

            // Pass the selected Course to the view
            return View(SelectedCourse);
        }

    }
}
