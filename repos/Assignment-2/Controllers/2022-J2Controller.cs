using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _2022_J2Controller : ControllerBase
    {
        /// <summary>
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
        /// 
        [HttpGet(template: "FergusonballRatings")]
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

            for (int i = 1; i <= totalNumberOfPlayers; i++)
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
    }
}
