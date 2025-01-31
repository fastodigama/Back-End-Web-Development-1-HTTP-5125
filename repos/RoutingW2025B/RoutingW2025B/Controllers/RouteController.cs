using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

namespace RoutingW2025B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        [HttpGet(template: "Get1")]
       
        public string Get()
        {
            return "Routing 1";

        }

        [HttpPost(template:"Post1")]

        public string Post1([FromBody]string Snack)
        {
            return "tour favorate snack is " + Snack;
        }
    
    }



}
