namespace OrderDiscountCalculator.Discounts;

public class PercentageDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _discountPercentage;

    public PercentageDiscountStrategy(decimal discountPercentage)
    {
        // Validate the discount percentage to ensure it's within a reasonable range (0-100).
        if (discountPercentage < 0 || discountPercentage > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(discountPercentage),
                "Discount percentage must be between 0 and 100.");
        }

        _discountPercentage = discountPercentage;
    }

    public decimal CalculateDiscount(decimal orderAmount)
    {
        return orderAmount * _discountPercentage / 100;
    }
}