using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _2022_J3Controller : ControllerBase
    {
        /// <summary>
        /// Receives an HTTP GET request with a query parameter containing the tuning instructions.
        /// </summary>
        /// <param name="tuningInstructions">A string of tuning instructions, including characters for notes,
        /// tightening (+), and loosening (-).</param>
        /// <returns>An HTTP response with the tuning instructions converted to a more readable format.</returns>
        /// <example>
        /// GET api/J3/HarpTuning?tuningInstructions=AFB+8HC-4
        /// -> A get method with a query parameter for tuning instructions.
        /// Query Parameters: tuningInstructions: AFB+8HC-4
        /// response:
        /// AFB tighten 8
        /// HC loosen 4
        /// </example>

        [HttpGet(template: "J3/HarpTuning")]
        public string HarpTuning(string tuningInstructions)
        {
            List<string> easierToReadTunings = new List<string>();

            for (int i = 0; i < tuningInstructions.Length; i++)
            {
                // Get the current character from the tuningInstructions string from 0
                char currentChar = tuningInstructions[i];

                if (char.IsUpper(currentChar))
                {

                    easierToReadTunings.Add(currentChar.ToString());
                }

                if (currentChar == '+')
                {
                    easierToReadTunings.Add(" tighten ");
                }

                if (currentChar == '-')
                {
                    easierToReadTunings.Add(" loosen ");
                }

                if (char.IsDigit(currentChar))
                {
                    easierToReadTunings.Add(currentChar.ToString());

                    // Check if the next character is an uppercase letter and not end of list
                    if (i + 1 < tuningInstructions.Length && char.IsUpper(tuningInstructions[i + 1]))
                    {
                        easierToReadTunings.Add("\n"); //add new line
                    }
                }
            }

            // Convert the list to a single string
            string result = string.Join("", easierToReadTunings);

            return result;
        }

    }
}
