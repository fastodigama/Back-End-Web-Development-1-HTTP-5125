using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Q8Controller : ControllerBase
    {
        /// <summary>
        /// Handles HTTP POST requests with a form-encoded body containing two parameters: small and large.
        /// </summary>
        /// <param name="small">The quantity of small items.</param>
        /// <param name="large">The quantity of large items.</param>
        /// <returns>An HTTP response with a body summarizing the order details including subtotal, tax, and total amount.
        /// The monetary values are formatted as currency using the current culture.</returns>
        /// <example>
        /// POST api/QX/squashfellows
        /// HEADERS: Content-Type: application/x-www-form-urlencoded
        /// FORM DATA: "small=2&large=3"
        /// -> A post method with a form-encoded request body.
        /// Body Content: small: 2, large: 3
        /// Response Body: "2 Small @ $25.50 = $51.00; 3 Large @ $40.50 = $121.50; Subtotal = $172.50; Tax = $22.43 HST; Total = $194.93"
        /// </example>
        /// <remarks>
        /// References:
        /// <list type="bullet">
        /// <item>
        /// <description><a href="https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#CFormatString">Standard Numeric Format Strings</a></description>
        /// </item>
        /// </list>
        /// </remarks>


        [HttpPost(template: "squashfellows")]
        [Consumes("application/x-www-form-urlencoded")]

        public string Post1([FromForm]double small, [FromForm]double large)
        {
            double smallUnitPrice = 25.50;
            double largeUnitPrice = 40.50;
            double HST = 0.13 ;

            double smallResault =Math.Round(smallUnitPrice * small,2);
            double largeResault = Math.Round(largeUnitPrice * large,2);
            double subtotal = Math.Round(smallResault + largeResault,2);
            double tax = Math.Round(subtotal * HST,2);
            double totalWithTax = Math.Round(subtotal + tax);

            //reference:https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#CFormatString

            // new CultureInfo("en-CA", false): Sets culture to English (CANADA) with default settings.(false) means it's affected by user operating system 
            // .NumberFormat: Formats numbers, currency based on culture. 

            NumberFormatInfo nfi = new CultureInfo("en-CA", false).NumberFormat;
            nfi.CurrencyGroupSeparator = "";

            return $@"{small} Small @ {smallUnitPrice.ToString("C", nfi)} = {smallResault.ToString("C", nfi)}; {large} Large @ {largeUnitPrice.ToString("C", nfi)} = {largeResault.ToString("C", nfi)}; Subtotal = {subtotal.ToString("C", nfi)}; Tax = {tax.ToString("C", nfi)} HST; Total = {totalWithTax.ToString("C", nfi)}";
        }


    }
}
