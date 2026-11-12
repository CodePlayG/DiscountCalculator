namespace OrderDiscountCalculator.Discounts
{
    public interface IDiscountStrategy
    {
        decimal CalculateDiscount(decimal orderAmount);
    }
}