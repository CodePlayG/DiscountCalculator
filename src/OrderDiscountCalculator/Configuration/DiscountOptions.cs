using OrderDiscountCalculator.Enums;

namespace OrderDiscountCalculator.Configuration
{
    public class DiscountOptions
    {
        //better approach to use new() in place of required property initialization for dictionary.
        //will protect from null reference exception if the dictionary is not initialized in the configuration file.
        public Dictionary<CustomerType, decimal> Rates { get; init; }
            = new();

    }
}