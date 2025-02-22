using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class J1Controller : ControllerBase
    {
        [HttpPost(template: "Delivedroid")]
        //[Consumes("application/x-www-form-urlencoded")]


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
    }
}
