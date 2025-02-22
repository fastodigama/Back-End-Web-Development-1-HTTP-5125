using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class _2022_J1Controller : ControllerBase
    {

        /// <summary>
        /// Receives an HTTP POST request with a form-encoded body containing two parameters:
        /// intputRegularBox and inputSmallBox.
        /// </summary>
        /// <param name="intputRegularBox">The number of regular-sized cupcake boxes.</param>
        /// <param name="inputSmallBox">The number of small-sized cupcake boxes.</param>
        /// <returns>An HTTP response with the number of leftover cupcakes after serving all students.</returns>
        /// <example>
        /// POST api/J1/CupcakeParty
        /// HEADERS: Content-Type: application/x-www-form-urlencoded
        /// FORM DATA: "intputRegularBox=3&inputSmallBox=4"
        /// -> A post method with a form-encoded request body.
        /// Body Content: intputRegularBox: 3, inputSmallBox: 4
        /// </example>
        [HttpPost(template: "CupcakeParty")]
        [Consumes("application/x-www-form-urlencoded")]

        public int CupcakeParty(int intputRegularBox, int inputSmallBox)
        {

            int numberOfStudents = 28;
            int regularBoxSize = 8;
            int smallBoxSize = 3;
            int leftOverCupcakes = 0;

            if (intputRegularBox >= 0 && inputSmallBox >= 0)
            {
                int totalNumberOfCupcakes = (intputRegularBox * regularBoxSize) + (inputSmallBox * smallBoxSize);
                leftOverCupcakes = totalNumberOfCupcakes - numberOfStudents;

            }

            return leftOverCupcakes;
        }
    }
}
