using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q7Controller : ControllerBase
    {
        /// <summary>
        /// Handles HTTP GET requests and returns a date that is a specified number of days from today.
        /// </summary>
        /// <param name="days">The number of days to add to (or subtract from) the current date.
        /// Positive values add days, negative values subtract days.</param>
        /// <returns>A string representing the resulting date in "yyyy-MM-dd" format.</returns>
        /// <example>
        /// GET api/Q7/timemachine?days=5
        /// -> A get method that returns a date that is 5 days from today.
        /// output: "2025-02-05" (assuming today is January 31, 2025)
        /// </example>
        /// <example>
        /// GET api/Q7/timemachine?days=-3
        /// -> A get method that returns a date that is 3 days before today.
        /// output : "2025-01-28" (assuming today is January 31, 2025)
        /// </example>
        /// /// <remarks>
        /// References:
        /// "https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1">Date and Time Format in C# Programming"        
        ///"https://stackoverflow.com/questions/11151976/subtract-days-from-a-datetime">Subtract Days from a DateTime"
      


        [HttpGet(template:"timemachine")]

        public string Get(int days)
        {
            // reference for the date time format: https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1
            // reference for substract the date: https://stackoverflow.com/questions/11151976/subtract-days-from-a-datetime

            DateTime currentDate = DateTime.Now;
            DateTime addDays = currentDate.AddDays(days);

          string  result = addDays.ToString("yyyy-MM-dd");

            return result;
            
            }
    }
}
