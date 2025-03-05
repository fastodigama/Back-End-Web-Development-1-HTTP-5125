using Microsoft.AspNetCore.Mvc;
using MyMVCWebApp.Models;

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

        //GET: localhost:xx/Flower/Terms -> A webpage that shows the terms and
        //conditions for the flower order

        public IActionResult Terms()
        {

            //pass information to the view
            int version = 5;
            ViewData["version"] = version;
            //Direct to /Views/Flower/Terms.cshml
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
            // TODO: pass that information along to the view

            FlowerOrder Order = new FlowerOrder();

            //Model Class way of sending data to ordersummary.chtml view
            Order.SubTotal = Math.Round(Subtotal, 2);
            Order.TaxAmount = Math.Round(Tax, 2);
            Order.Label = "HST";
            Order.OrderTotal = Math.Round(OrderTotal, 2);
            Order.DeliveryAddress = OrderAddress;
            Order.NumFlowers = NumFlowers;
            Order.FlowerType = FlowerType;


            //View Data way to send data to ordersummary.chtml view
            //ViewData["Subtotal"] = Math.Round(Subtotal,2);
            //ViewData["Tax"] = Math.Round(Tax,2);
            //ViewData["OrderTotal"] = Math.Round(OrderTotal, 2);
            //ViewData["OrderAddress"] = OrderAddress;
            //ViewData["NumFlowers"] = NumFlowers;
            //ViewData["FlowerType"] = FlowerType;

            // Direct to /Views/Flower/OrderSummary.cshtml
            //return View(); this return is empty when using View Data
            return View(Order);

        }


        public IActionResult OrderHistory()
        {
            FlowerOrder Order1 = new FlowerOrder();
            Order1.OrderID = 50;
            Order1.OrderTotal = 100;
            Order1.DeliveryAddress = "1298 Sweetbirch crt";

            FlowerOrder Order2 = new FlowerOrder();

            Order2.OrderID = 51;
            Order2.OrderTotal = 40;
            Order2.DeliveryAddress = "47 Mississauga Valley blvrd";

            List<FlowerOrder> listOfOrders = new List<FlowerOrder>() {Order1, Order2};

            



           
            return View(listOfOrders);
        }

    }
}
