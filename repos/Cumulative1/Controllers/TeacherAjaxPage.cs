using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class TeacherAjaxPage : Controller
    {
        public IActionResult AJAXTeacherAdd()
        {
            return View();
        }

        public IActionResult AJAXTeacherList()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
