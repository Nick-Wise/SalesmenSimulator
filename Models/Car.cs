namespace SalesmenSimulator.Models;

public class Car(CarType type, CarCondition condition)
{
    public CarType Type = type;
    public CarCondition Condition = condition;
    public decimal BuyPrice => CalculateBuyPrice();

    public decimal SellPrice { get; set; }

    private decimal CalculateBuyPrice()
    {
        decimal basePrice = Type switch
        {
            CarType.Sedan => 10000,
            CarType.Coupe => 35000,
            CarType.Truck => 15000,
            CarType.Suv => 20000,
            _ => throw new InvalidOperationException($"Unexpected Type: {Type}")
        };

        decimal conditionMultiplier = Condition switch
        {
            CarCondition.D => 0.6m,
            CarCondition.C => 0.8m,
            CarCondition.B => 1.0m,
            CarCondition.A => 1.2m,
            CarCondition.S => 1.4m,
            _ => throw new InvalidOperationException($"Invalid Condition: {Condition}")
        };

        return basePrice * conditionMultiplier;
    }
}
