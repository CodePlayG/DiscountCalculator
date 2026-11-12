using OrderDiscountCalculator.Enums;

namespace OrderDiscountCalculator.Models
{
    public class Order
    {
        public required string OrderId { get; set; }
        public required string CustomerName { get; set; }
        public decimal OrderAmount { get; set; }
        public CustomerType CustomerType { get; set; }
    }
}