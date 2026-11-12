using OrderDiscountCalculator.Discounts;

namespace OrderDiscountCalculator.Tests.Discounts;

public class PercentageDiscountStrategyTests
{
    [Theory]
    [InlineData(1000, 5, 50)]
    [InlineData(1000, 10, 100)]
    [InlineData(1000, 15, 150)]
    [InlineData(2000, 10, 200)]
    public void CalculateDiscount_ShouldReturnExpectedValue(decimal orderAmount,
                                                            decimal percentage,
                                                            decimal expectedDiscount)
    {
        var strategy = new PercentageDiscountStrategy(percentage);
        var discount = strategy.CalculateDiscount(orderAmount);

        // Assert
        Assert.Equal(expectedDiscount, discount);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    [InlineData(150)]
    public void Constructor_ShouldThrow_WhenPercentageIsInvalid(decimal percentage)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PercentageDiscountStrategy(percentage));
    }

    [Fact]
    public void Constructor_ShouldAllow_ZeroPercentage()
    {
        var strategy = new PercentageDiscountStrategy(0);
        var discount = strategy.CalculateDiscount(1000);

        Assert.Equal(0, discount);
    }

    [Fact]
    public void Constructor_ShouldAllow_OneHundredPercent()
    {
        var strategy = new PercentageDiscountStrategy(100);

        var discount = strategy.CalculateDiscount(1000);

        Assert.Equal(1000, discount);
    }
}