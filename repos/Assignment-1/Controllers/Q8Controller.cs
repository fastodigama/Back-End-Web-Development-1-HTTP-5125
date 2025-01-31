using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q8Controller : ControllerBase
    {
        /// <summary>
        /// Receives an HTTP POST request with a form-encoded body containing two parameters: small and large.
        /// </summary>
        /// <param name="small">The quantity of small items.</param>
        /// <param name="large">The quantity of large items.</param>
        /// <returns>An HTTP response with a body summarizing the order details including subtotal, tax, and total amount.</returns>
        /// <example>
        /// POST api/Q8/squashfellows
        /// HEADERS: Content-Type: application/x-www-form-urlencoded
        /// FORM DATA: "small=2&large=3"
        /// -> A post method with a form-encoded request body. 
        /// Body Content: small: 2, large: 3
        /// </example>

        [HttpPost(template: "squashfellows")]
        [Consumes("application/x-www-form-urlencoded")]

        public string Post1([FromForm]double small, [FromForm]double large)
        {
            double smallUnitPrice = 25.50;
            double largeUnitPrice = 40.50;
            double HST = 0.13 ;

            double smallResault = smallUnitPrice * small;
            double largeResault = largeUnitPrice * large;
            double subtotal = smallResault + largeResault;
            double tax = subtotal * HST;
            double totalWithTax = subtotal + tax;

            return $"{small} Small @ $25.50 = ${smallResault}; {large} Large @ $40.50 = ${largeResault}; Subtotal = ${subtotal}; Tax = ${tax} HST; Total = ${totalWithTax}";
        }


    }
}
