using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q4Controller : ControllerBase
    {
        /// <summary>
        /// Controller for handling knock-knock jokes.
        /// </summary>
        /// <summary>
        /// Handles HTTP POST requests and returns the response "Who’s there?".
        /// </summary>
        /// <returns>A string response "Who’s there?".</returns>
        /// <example>
        /// POST api/Q4/knockknock
        /// -> A post method that returns the response "Who’s there?".
        /// Response Body: "Who’s there?"
        /// </example>

        [HttpPost("knockknock")]
        public string Post1()
        {
            return "Who’s there?";
        }
    }
}
