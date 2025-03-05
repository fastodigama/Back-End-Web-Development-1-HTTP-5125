namespace MyMVCWebApp.Models
{
    public class FlowerOrder
    {
        //What are the peices of information that describe the flower order?

        //Order ID

        public int OrderID { get; set; }

        //the number of flowers

        public int NumFlowers { get; set; }

        //the address to send the flowers to

        public string DeliveryAddress { get; set; }

        //the flower type

        public string FlowerType { get; set; }

        //the subtotal of the order

        public decimal SubTotal { get; set; }

        //the tax amount of the order

        public decimal TaxAmount { get; set; }


        //the label of the order

        public string Label { get; set; }

        //the final amount 
        public decimal OrderTotal { get; set; }
    }
}
