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



        
        [HttpGet]

        public IActionResult NewCourse()
        {
            //this method to redirect to NewCourse.cshtml when the user click add new course

            return View();
        }

        [HttpPost]

        public IActionResult CreateCourse(Courses NewCourse)
        {
            //recieves new course informaion from add course webpage
            _APIcontroller.AddCourse(NewCourse);

            return RedirectToAction("CoursesList");
        }

        [HttpGet]
        public IActionResult CourseDeleteConfirm(int Id)
        {
            //first find the course
            Courses SelectedCourse = _APIcontroller.FindCourse(Id);

            //Then send it to confirm
            return View(SelectedCourse);
        }

        
        [HttpPost]

        public IActionResult DeleteCourse(int Id)
        {
            //send courseid for deletion
            _APIcontroller.DeleteCourse(Id);

            return RedirectToAction("CoursesList");


        }

    }
}
