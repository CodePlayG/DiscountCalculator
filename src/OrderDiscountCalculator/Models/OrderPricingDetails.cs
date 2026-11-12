namespace OrderDiscountCalculator.Models
{
    public class OrderPricingDetails
    {
        public required string OrderId { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal FinalPrice { get; set; }
    }
}