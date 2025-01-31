using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q3Controller : ControllerBase
    {
        /// <summary>
        /// Controller for handling cube calculations.
        /// </summary>
        /// <summary>
        /// Handles HTTP GET requests and returns the cube of the given integer.
        /// </summary>
        /// <param name="id">The integer to be cubed.</param>
        /// <returns>The cube of the given integer.</returns>
        /// <example>
        /// GET api/Q3/cube/3
        /// -> A get method that returns the cube of the given integer.
        /// Response Body: 27
        /// </example>

        [HttpGet(template: "cube/{id}")]

        public int Get(int id)
        {
            int cube = id * id * id;
            return cube;
        }
    }
}
