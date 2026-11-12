using OrderDiscountCalculator.Configuration;
using OrderDiscountCalculator.Discounts;
using OrderDiscountCalculator.Enums;

namespace OrderDiscountCalculator.Factories;

public class DiscountStrategyFactory
{
    private readonly DiscountOptions _options;

    public DiscountStrategyFactory(DiscountOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (!options.Rates.Any())
        {
            throw new InvalidOperationException(
                "No discount rates have been configured.");
        }

        _options = options;
    }

    public IDiscountStrategy CreateStrategy(CustomerType customerType)
    {
        // Validate the customer type to ensure it's a defined enum value.
        if (!Enum.IsDefined(customerType))
        {
            throw new ArgumentException(
                $"Invalid customer type '{customerType}'.",
                nameof(customerType));
        }

        if (!_options.Rates.TryGetValue(customerType, out var rate))
        {
            throw new InvalidOperationException(
                $"Discount not configured for '{customerType}'.");
        }
        //Implementation of the factory method to create a (for now Percentage discount) strategy based on the customer type and configured rates.
        return new PercentageDiscountStrategy(rate);
    }
}