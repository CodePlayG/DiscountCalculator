using OrderDiscountCalculator.Factories;
using OrderDiscountCalculator.Models;

namespace OrderDiscountCalculator.Services;

public class OrderCalculator
{
    private readonly DiscountStrategyFactory _factory;

    public OrderCalculator(DiscountStrategyFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public OrderPricingDetails Calculate(Order order)
    {
        Validate(order);

        var strategy =
            _factory.CreateStrategy(order.CustomerType);

        var discountAmount =
            strategy.CalculateDiscount(order.OrderAmount);

        var finalPrice =
            order.OrderAmount - discountAmount;

        return new OrderPricingDetails
        {
            OrderId = order.OrderId,
            OriginalPrice = order.OrderAmount,
            DiscountAmount = discountAmount,
            FinalPrice = finalPrice
        };
    }

    //validate the order to ensure all required fields are present and valid.
    private static void Validate(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        if (string.IsNullOrWhiteSpace(order.OrderId))
        {
            throw new ArgumentException(
                "Order ID is required.",
                nameof(order.OrderId));
        }

        if (string.IsNullOrWhiteSpace(order.CustomerName))
        {
            throw new ArgumentException(
                "Customer Name is required.",
                nameof(order.CustomerName));
        }

        if (order.OrderAmount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(order.OrderAmount),
                "Order Amount must be greater than zero.");
        }
    }
}