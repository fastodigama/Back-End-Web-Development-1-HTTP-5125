using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q1Controller : ControllerBase
    {
        /// <summary>
        /// Controller for handling welcome messages.
        /// </summary>
        /// <summary>
        /// Handles HTTP GET requests and returns a welcome message.
        /// </summary>
        /// <returns>A welcome message string.</returns>
        /// <example>
        /// GET api/Q1/welcome
        /// -> A get method that returns a welcome message.
        /// Response Body: "Welcome to 5125!"
        /// </example>


        [HttpGet(template: "welcome")]

        public string Welcome()

        {
            return "Welcome to 5125!";
        }
    }
}

