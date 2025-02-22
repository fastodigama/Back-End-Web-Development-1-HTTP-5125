using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J2Controller : ControllerBase
    {
        [HttpGet(template: "ChiliPeppers")]

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

    }
}
