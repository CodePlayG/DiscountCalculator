# DiscountCalculator
Calculate discount for order price base on Customer Type (VIP, Regular, Premium) with Strategy and factory pattern and following SOILD design pattern

Order Discount Calculator
The Order Discount Calculator is a .NET 8 console application that calculates customer discounts and final order prices based on customer type.
Currently supported customer types:
•	Regular: 5% discount
•	Premium: 10% discount
•	VIP: 15% discount
The application calculates:
•	Discount Amount
•	Final Price after discount
________________________________________
How to Run the Solution
Prerequisites
•	.NET 8 SDK
•	Visual Studio 2022 or later (recommended)
Build the Solution
dotnet build
Run the Application
dotnet run --project src/OrderDiscountCalculator
Run Unit Tests
dotnet test
________________________________________
Design Approach
The solution separates responsibilities into dedicated components:
•	Models contain application data structures.
•	OrderCalculator contains order validation and pricing calculation logic.
•	DiscountStrategyFactory creates the appropriate discount strategy based on customer type.
•	IDiscountStrategy abstracts discount calculation behavior.
•	PercentageDiscountStrategy implements percentage-based discounts.
•	DiscountOptions stores configurable discount rates loaded from configuration.
Discount percentages are maintained outside business logic, making them easy to change without modifying calculation code.
________________________________________
Assumptions
•	Order ID is mandatory.
•	Customer Name is mandatory.
•	Order Amount must be greater than zero.
•	Discount percentages must be between 0 and 100.
•	Every customer type must have a configured discount rate.
•	Customer types are represented using an enum.
•	Invalid orders or configuration values result in exceptions.
________________________________________
Design Patterns Used
Strategy Pattern
The application uses the Strategy Pattern through IDiscountStrategy.
This allows future discount calculation behaviors to be added without changing the existing order calculation logic.
Example future strategies:
•	Fixed amount discount
•	Tier-based discount
•	Promotional discount
•	Seasonal discount
Factory Pattern
The DiscountStrategyFactory is responsible for creating the appropriate discount strategy for a customer type.
This centralizes strategy creation and keeps the OrderCalculator independent of concrete implementations.
________________________________________
Extensibility
New customer types can be introduced by:
1.	Adding a new value to the CustomerType enum.
2.	Adding a discount rate in configuration.
3.	Optionally creating a new strategy implementation if a different discount calculation rule is required.
The OrderCalculator remains unchanged, satisfying the requirement to support future discount types without modifying the existing calculation logic.

