using OrderDiscountCalculator.Configuration;
using OrderDiscountCalculator.Discounts;
using OrderDiscountCalculator.Enums;
using OrderDiscountCalculator.Factories;

namespace OrderDiscountCalculator.Tests.Factories;

public class DiscountStrategyFactoryTests
{
    private readonly DiscountOptions _options;

    public DiscountStrategyFactoryTests()
    {
        _options = new DiscountOptions
        {
            Rates = new Dictionary<CustomerType, decimal>
            {
                { CustomerType.Regular, 5 },
                { CustomerType.Premium, 10 },
                { CustomerType.VIP, 15 }
            }
        };
    }

    [Fact]
    public void CreateStrategy_ShouldReturnPercentageDiscountStrategy()
    {
        // Arrange
        var factory = new DiscountStrategyFactory(_options);

        // Act
        var strategy =
            factory.CreateStrategy(CustomerType.Regular);

        // Assert
        Assert.IsType<PercentageDiscountStrategy>(strategy);
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenConfigurationIsNull()
    {
        Assert.Throws<ArgumentNullException>(
            () => new DiscountStrategyFactory(null!));
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenConfigurationIsEmpty()
    {
        var options = new DiscountOptions();

        Assert.Throws<InvalidOperationException>(
            () => new DiscountStrategyFactory(options));
    }

    [Fact]
    public void CreateStrategy_ShouldThrow_WhenCustomerTypeIsInvalid()
    {
        var factory =
            new DiscountStrategyFactory(_options);

        Assert.Throws<ArgumentException>(
            () => factory.CreateStrategy(
                (CustomerType)999));
    }

    [Fact]
    public void CreateStrategy_ShouldThrow_WhenCustomerTypeNotConfigured()
    {
        var options = new DiscountOptions
        {
            Rates = new Dictionary<CustomerType, decimal>
            {
                { CustomerType.Regular, 5 }
            }
        };

        var factory = new DiscountStrategyFactory(options);

        Assert.Throws<InvalidOperationException>(
            () => factory.CreateStrategy(
                CustomerType.Premium));
    }
}