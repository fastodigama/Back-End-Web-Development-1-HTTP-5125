using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

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

        //GET: TeacherPage/New -> ask the user to write the new teacher information
        //ASK THE USER WHAT INFORMATION THEY WANT TO ENTER

        [HttpGet]
        public IActionResult New()
        {
            //drirects to /Views/TeacherPage/New.cshtml
            return View();
        }

        //POST: TeacherPage/Create -> Recieves information about the teacher and goes to the list of teachers

        //WHEN THE USER FINISH WRITING THE TEACHER INFORMATION, THEN THEY CAN SUBMIT THE FORM TO THE DATABASE

        //HEADER: Content-Type: application/x-www-form-urlencoded

        //FORM DATA: TeacherFname=Fadel&TeacherLname=Matar&EmployeeNumber=T345&HireDate=2023-10-01&Salary=95.55

        [HttpPost]

        public IActionResult CreateTeacher(Teacher NewTeacher)
        {
            //add teacher to the database

            _APIcontroller.AddTeacher(NewTeacher);

            //return to the teacher list
            return RedirectToAction("List", NewTeacher.TeatcherId);
        }

        //GET : /TeacherPage/DeleteConfirm/{Id} -> A webpage that asks the user to confirm deletion of a teacher

        public IActionResult DeleteConfirm(int Id)
        {
            Teacher SelectedTeacher =  _APIcontroller.FindTeacher(Id);



            return View(SelectedTeacher);

        }

        //POST: /TeacherPage/Delete/{Id} -> Deletes the teacher from the database
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            //delete the teacher from the database
            _APIcontroller.DeleteTeacher(Id);

            //redirect to the list of teachers after deleting the teacher
            return RedirectToAction("List");
        }

    }
}
