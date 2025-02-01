using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q2Controller : ControllerBase
    {
        /// <summary>
        /// Controller for handling greeting messages.
        /// </summary>
        /// <summary>
        /// Handles HTTP GET requests and returns a greeting message.
        /// </summary>
        /// <param name="name">The name of the person to greet.</param>
        /// <returns>A greeting message string.</returns>
        /// <example>
        /// GET api/Q2/Greeting?name=Fadel
        /// -> A get method that returns a greeting message.
        /// output: "Hi Fadel!"
        /// </example>


        [HttpGet(template:"Greeting")]

        public string Get(string name)

        {
            return $"Hi {name}!";
        }
    }
}
