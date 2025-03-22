using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        private TeacherAPIController _APIcontroller;

        public TeacherPageController (TeacherAPIController APIcontroller)
        {
            _APIcontroller = APIcontroller;

        }

        //GET: TeacherPage/List -> A webpage that lists teachers

        [HttpGet]

        //List can optionally recieve from and to dates
        public IActionResult List(DateTime? FromDate, DateTime? ToDate)
        {
            //FromDate ??=  DateTime.MinValue; // Default to the earliest possible date if not provided
            //ToDate ??=  DateTime.MaxValue;   // Default to the latest possible date if not provided

            FromDate ??= default;
            ToDate ??= default;
            // Pass the date range to the ListTeachers method
            List<Teacher> Teachers = _APIcontroller.ListTeachers(FromDate.Value, ToDate.Value);
            return View(Teachers);
        }


        //GET: TeacherPage/Show -> A web page that displays all information on a teacher
        [HttpGet]

        public IActionResult Show(int ID)
        {
            Teacher SelectedTeacher = _APIcontroller.FindTeacher(ID);

            return View(SelectedTeacher);

        }
    }
}
