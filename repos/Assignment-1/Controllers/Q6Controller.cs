using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q6Controller : ControllerBase
    {
        /// <summary>
        /// Handles HTTP GET requests and returns the area of a hexagon.
        /// </summary>
        /// <param name="side">The length of one side of the hexagon.</param>
        /// <returns>The area of the hexagon if the side length is positive; otherwise, returns 0.</returns>
        /// <example>
        /// GET api/Q6/hexagon?side=5
        /// -> A get method that returns the area of the hexagon with the given side length.
        /// Response Body: 64.9519052838329
        /// </example>

        [HttpGet(template:"hexagon")]

        public double Hexagon(double side)
        {

            if (side > 0)
            {
                double area = (3 * Math.Sqrt(3)) / 2 * Math.Pow(side, 2);
                return area;
            }
            else
            {
                return 0;

            }
            
        }
    }
}
