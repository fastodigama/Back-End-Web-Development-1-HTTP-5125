using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q5Controller : ControllerBase
    {
        /// <summary>
        /// Handles HTTP POST requests and returns a message with the provided secret.
        /// </summary>
        /// <param name="secret">The secret integer to be included in the message.</param>
        /// <returns>A string message revealing the secret.</returns>
        /// <example>
        /// POST api/Q5/Secret
        /// HEADERS: Content-Type: application/json
        /// BODY: { "secret": 5 }
        /// -> A post method that returns a message with the provided secret.
        /// Response Body: "Shh.. the secret is 5"
        /// </example>

        [HttpPost(template:"Secret")]

        public string Post1([FromBody]int secret)
        {
           string msg = $"Shh.. the secret is {secret}";
            return msg;

        }
        

        
    }
}
