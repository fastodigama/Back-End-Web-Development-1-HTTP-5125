using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MyMVCWebApp.Controllers
{
    public class FlowerController : Controller
    {
        //Input type of request GET: localhost:XX//Flower/welcome -> A webpage that welcomes the user
        //to our flower store
        // It is by default an [HttpGet] request
        public IActionResult Welcome()
        {
            //Directs to /Views/Flower/Welcome.cshtml
            return View();
        }

        //GET Request: localhost:XX/Flower/Order -> A webpage that asks the user what
        //kinds of flowers they want to buy

        public IActionResult Order()
        {
            // Direct to /Views/Flower/Order.cshtml
            return View();

        }

        //POST: localhost:XX/Flower/OrderSummary.cshtml
        //Header: content-Type: application/x-www-form-urlencoded
        //FORM DATA: OrderAddr={OrderAddr}&NumFlowers={NumFlowers}&FlowerType={FlowerType}

        [HttpPost]
        public IActionResult OrderSummary(string OrderAddress, int NumFlowers, string FlowerType)
        {

            // TODO: Build view of OrderSummary

            // TODO: Confirm we received the data

            Debug.WriteLine("The addr is" + OrderAddress);
            Debug.WriteLine("The number of flowers are " + NumFlowers);
            Debug.WriteLine("The type of flowers are " + FlowerType);


            // TODO: Calculate the order total

            decimal Subtotal = 0;
            decimal perFlowerAmount = 0;

            if(FlowerType == "Roses")
            {
                perFlowerAmount = 4.00M;
            }
            else if(FlowerType == "Tulips")
            {
                perFlowerAmount = 3.50M;

            }
            else if(FlowerType == "Jasmin")
               
            {
                perFlowerAmount = 6.00M;
            }

            Subtotal = perFlowerAmount * NumFlowers;
            decimal Tax = Subtotal * 0.13M; //HST

            decimal OrderTotal = Subtotal + Tax;

            ViewData["Subtotal"] = Math.Round(Subtotal,2);
            ViewData["Tax"] = Math.Round(Tax,2);
            ViewData["OrderTotal"] = Math.Round(OrderTotal, 2);



                // TODO: pass that information along to the view

            ViewData["OrderAddress"] = OrderAddress;
            ViewData["NumFlowers"] = NumFlowers;
            ViewData["FlowerType"] = FlowerType;

            // Direct to /Views/Flower/OrderSummary.cshtml
            return View();

        }

    }
}
