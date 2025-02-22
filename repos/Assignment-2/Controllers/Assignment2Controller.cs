using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Assignment2Controller : ControllerBase
    {
        /// <summary>
        /// Receives an HTTP POST request with a form-encoded body containing two parameters:
        /// Collisions and Deliveries.
        /// </summary>
        /// <param name="Collisions">The number of collisions with obstacles.</param>
        /// <param name="Deliveries">The number of successful deliveries.</param>
        /// <returns>
        /// An HTTP response with a body summarizing the final score,
        /// including delivered points, lost points, and any bonus points.
        /// </returns>
        /// <example>
        /// POST api/J1/Delivedroid
        /// HEADERS: Content-Type: application/x-www-form-urlencoded
        /// FORM DATA: "Collisions=2&Deliveries=3"
        /// -> A post method with a form-encoded request body. 
        /// Body Content: Collisions: 2, Deliveries: 3
        /// </example>

        [HttpPost(template: "J1/Delivedroid")]
        [Consumes("application/x-www-form-urlencoded")]

        public int Delivedroid([FromForm] int Collisions, [FromForm] int Deliveries)
        {
            int pointsIfPackageDelivered = 50;
            int pointsLostIfCollisionWithObstacle = 10;

            int bonusPoints = 500;
            int deliveredScore = Deliveries * pointsIfPackageDelivered;
            int lostScore = pointsLostIfCollisionWithObstacle * Collisions;
            int finalScore = deliveredScore - lostScore;

            if (Deliveries > Collisions)
            {
                finalScore += bonusPoints;

            }

            return finalScore;


        }

        /// <summary>
        /// Receives an HTTP GET request with a query parameter containing the list of ingredients.
        /// </summary>
        /// <param name="Ingredients">A comma-separated string of ingredients.</param>
        /// <returns>An HTTP response with the spiciness level of the ingredients combined.</returns>
        /// <example>
        /// GET api/J2/ChiliPeppers?Ingredients=Poblano,Serrano,Thai
        /// -> A get method with a query parameter for ingredients.
        /// Query Parameters: Ingredients: Poblano,Serrano,Thai
        /// </example>
        [HttpGet(template: "J2/ChiliPeppers")]

        public int ChiliPeppers(string Ingredients)
        {
            string[] inputIngredients = Ingredients.Split(',');

            int currentSpiciness = 0;


            Dictionary<string, int> spicinessSHU = new Dictionary<string, int>()
                {
                    {"Poblano" , 1500 },
                    {"Mirasol" , 6000 },
                    {"Serrano" , 15500 },
                    {"Cayenne" , 40000 },
                    {"Thai"    , 75000 },
                    {"Habanero", 125000 }
                };

            for (int index = 0; index < inputIngredients.Length; index++)
            {
                foreach (KeyValuePair<string, int> spice in spicinessSHU)
                {
                    if (inputIngredients[index] == spice.Key)
                    {
                        currentSpiciness += spice.Value;
                    }
                }
            }

            return currentSpiciness;

        }


        /// <summary>
        /// This is from ccc of 2022
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

        [HttpPost(template: "J1/CupcakeParty")]
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

        /// <summary>
        /// This is from ccc of 2022
        /// Receives an HTTP GET request with a query parameter containing the fergusonball game scores and fouls
        /// for each player.
        /// </summary>
        /// <param name="fergusonballGame">A string containing the number of players and their respective
        /// scores and fouls, separated by spaces or commas.</param>
        /// <returns>response with the number of players who have a final score greater than 40.</returns>
        /// <example>
        /// GET api/J2/FergusonballRatings?fergusonballGame=4,50,2,60,1,45,0,55,3
        /// -> A get method with a query parameter for fergusonball game.
        /// Query Parameters: fergusonballGame: 4,50,2,60,1,45,0,55,3
        /// </example>


        [HttpGet(template: "J2/FergusonballRatings")]
        public string FergusonballRatings(string fergusonballGame)
        {
            string[] scoreStrings = fergusonballGame.Split(' ', ','); // input is split either by space or comma

            //reference: https://learn.microsoft.com/en-us/dotnet/api/system.array.convertall?view=net-7.0&form=MG0AV3

            int[] scoreSheet = Array.ConvertAll(scoreStrings, int.Parse); // Converting the array items to integer so i can use them in my calculations

            int totalNumberOfPlayers = scoreSheet[0]; //taking the fist digit as number of players

            int starsAwarded = 5;
            int starsTaken = 3;
            int arrayPosition = 1; // using this variable to skip through the positions on the array to identify each player points and fouls
            int playerStarRatings = 0;

            //for loop to repete the process of calculation based on the number of players

            for (int i = 1; i <= totalNumberOfPlayers ; i++)
            {
                                
                int playerScore = scoreSheet[arrayPosition];                
                int playerFouls = scoreSheet[arrayPosition + 1];                
                playerScore = playerScore * starsAwarded;                
                playerFouls = playerFouls * starsTaken;                
                int playerFinalScore = playerScore - playerFouls;

                //cumulating the number of players who score above 40
                if (playerFinalScore > 40)
                {
                    playerStarRatings += 1;
                }
                arrayPosition += 2; //skip 2 positions from the last position. i.e start with 1, then 1 + 2 the next rount will start from array postion 3 and then 5 and so on
            }
            
            if (totalNumberOfPlayers == playerStarRatings)
            {
                return $"{playerStarRatings}+";
            }
            else
            {
                return playerStarRatings.ToString();
            }
        }

        /// <summary>
        /// This is from ccc of 2022
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
