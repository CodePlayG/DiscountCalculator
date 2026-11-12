using Microsoft.Extensions.Configuration;
using OrderDiscountCalculator.Configuration;
using OrderDiscountCalculator.Enums;
using OrderDiscountCalculator.Factories;
using OrderDiscountCalculator.Models;
using OrderDiscountCalculator.Services;


var calculator = CreateOrderCalculator();
var orders = CreateSampleOrders();

RunCalculator(calculator, orders);

static OrderCalculator CreateOrderCalculator()
{
    var discountOptions = LoadDiscountConfiguration();
    var factory = new DiscountStrategyFactory(discountOptions);

    return new OrderCalculator(factory);
}

static DiscountOptions LoadDiscountConfiguration()
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

    var options = new DiscountOptions();
    var discountRatesSection = configuration.GetSection("DiscountRates");

    if (!discountRatesSection.Exists())
    {
        throw new InvalidOperationException("DiscountRates section is missing.");
    }

    foreach (var child in discountRatesSection.GetChildren())
    {
        if (!Enum.TryParse<CustomerType>(child.Key,true,out var customerType))
        {
            throw new InvalidOperationException(
                $"Invalid customer type '{child.Key}' found in configuration.");
        }

        if (!decimal.TryParse(child.Value, out var rate))
        {
            throw new InvalidOperationException($"Invalid discount rate for '{child.Key}'.");
        }

        options.Rates.Add(customerType, rate);
    }

    if (!options.Rates.Any())
    {
        throw new InvalidOperationException("No discount rates configured.");
    }

    return options;
}

//Sample orders for demonstration purposes
static List<Order> CreateSampleOrders()
{
    return
    [
        new()
        {
            OrderId = "ORD001",
            CustomerName = "John Doe",
            CustomerType = CustomerType.Regular,
            OrderAmount = 1000m
        },

        new()
        {
            OrderId = "ORD002",
            CustomerName = "Sarah Smith",
            CustomerType = CustomerType.Premium,
            OrderAmount = 2000m
        },

        new()
        {
            OrderId = "ORD003",
            CustomerName = "David Brown",
            CustomerType = CustomerType.VIP,
            OrderAmount = 3000m
        }
    ];
}

static void RunCalculator(OrderCalculator calculator,IEnumerable<Order> orders)
{
    foreach (var order in orders)
    {
        try
        {
            var pricing = calculator.Calculate(order);
            
            DisplayPricing(pricing);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Error processing order '{order.OrderId}': {ex.Message}");
        }
    }
}

// Display the pricing details for an order
static void DisplayPricing(OrderPricingDetails pricing)
{
    Console.WriteLine("-----------------------------------");
    Console.WriteLine($"Order Id       : {pricing.OrderId}");
    Console.WriteLine($"Original Price : {pricing.OriginalPrice:C}");
    Console.WriteLine($"Discount       : {pricing.DiscountAmount:C}");
    Console.WriteLine($"Final Price    : {pricing.FinalPrice:C}");
}