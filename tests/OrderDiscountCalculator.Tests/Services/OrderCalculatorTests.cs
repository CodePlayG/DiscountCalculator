using OrderDiscountCalculator.Configuration;
using OrderDiscountCalculator.Enums;
using OrderDiscountCalculator.Factories;
using OrderDiscountCalculator.Models;
using OrderDiscountCalculator.Services;

namespace OrderDiscountCalculator.Tests.Services;

public class OrderCalculatorTests
{
    private readonly OrderCalculator _calculator;

    public OrderCalculatorTests()
    {
        var options = new DiscountOptions
        {
            Rates = new Dictionary<CustomerType, decimal>
            {
                { CustomerType.Regular, 5m },
                { CustomerType.Premium, 10m },
                { CustomerType.VIP, 15m }
            }
        };
        var factory = new DiscountStrategyFactory(options);

        _calculator = new OrderCalculator(factory);
    }

    [Fact]
    public void Calculate_ShouldApplyRegularDiscount()
    {
        var order = new Order
        {
            OrderId = "ORD001",
            CustomerName = "John",
            CustomerType = CustomerType.Regular,
            OrderAmount = 1000m
        };

        var result = _calculator.Calculate(order);

        Assert.Equal(50m, result.DiscountAmount);
        Assert.Equal(950m, result.FinalPrice);
    }

    [Fact]
    public void Calculate_ShouldApplyPremiumDiscount()
    {
        var order = new Order
        {
            OrderId = "ORD002",
            CustomerName = "John",
            CustomerType = CustomerType.Premium,
            OrderAmount = 1000m
        };

        var result = _calculator.Calculate(order);

        Assert.Equal(100m, result.DiscountAmount);
        Assert.Equal(900m, result.FinalPrice);
    }

    [Fact]
    public void Calculate_ShouldApplyVipDiscount()
    {
        var order = new Order
        {
            OrderId = "ORD003",
            CustomerName = "John",
            CustomerType = CustomerType.VIP,
            OrderAmount = 1000m
        };

        var result = _calculator.Calculate(order);

        Assert.Equal(150m, result.DiscountAmount);
        Assert.Equal(850m, result.FinalPrice);
    }

    [Fact]
    public void Calculate_ShouldThrow_WhenOrderIsNull()
    {
        Assert.Throws<ArgumentNullException>(
            () => _calculator.Calculate(null!));
    }

    [Fact]
    public void Calculate_ShouldThrow_WhenOrderIdIsEmpty()
    {
        var order = new Order
        {
            OrderId = "",
            CustomerName = "John",
            CustomerType = CustomerType.Regular,
            OrderAmount = 1000m
        };

        Assert.Throws<ArgumentException>(
            () => _calculator.Calculate(order));
    }

    [Fact]
    public void Calculate_ShouldThrow_WhenCustomerNameIsEmpty()
    {
        var order = new Order
        {
            OrderId = "ORD001",
            CustomerName = "",
            CustomerType = CustomerType.Regular,
            OrderAmount = 1000m
        };

        Assert.Throws<ArgumentException>(() => _calculator.Calculate(order));
    }

    [Fact]
    public void Calculate_ShouldThrow_WhenOrderAmountIsZero()
    {
        var order = new Order
        {
            OrderId = "ORD001",
            CustomerName = "John",
            CustomerType = CustomerType.Regular,
            OrderAmount = 0m
        };

        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(order));
    }

    [Fact]
    public void Calculate_ShouldThrow_WhenOrderAmountIsNegative()
    {
        var order = new Order
        {
            OrderId = "ORD001",
            CustomerName = "John",
            CustomerType = CustomerType.Regular,
            OrderAmount = -100m
        };

        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(order));
    }

    [Fact]
    public void Calculate_ShouldThrow_WhenCustomerTypeIsInvalid()
    {
        var order = new Order
        {
            OrderId = "ORD001",
            CustomerName = "John",
            CustomerType = (CustomerType)999,
            OrderAmount = 1000m
        };

        Assert.Throws<ArgumentException>(() => _calculator.Calculate(order));
    }
}