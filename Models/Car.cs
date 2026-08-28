
namespace SalesmenSimulator.Models;

public class Car(CarType type)
{
    public CarType Type => type;
    public CarCondition Condition => GetCondition();
    public decimal BuyPrice => CalculateBuyPrice();

    public decimal SellPrice { get; set; }

    private CarCondition GetCondition()
    {
        int randomNum = Random.Shared.Next(100);
        return randomNum switch
        {
            < 10 => CarCondition.D,
            < 35 => CarCondition.C,
            < 65 => CarCondition.B,
            < 90 => CarCondition.A,
            _ => CarCondition.S,

        };
    }

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

        double conditionMultiplier = Condition switch
        {
            CarCondition.D => 0.6,
            CarCondition.C => 0.8,
            CarCondition.B => 0.1,
            CarCondition.A => 1.2,
            CarCondition.S => 1.4,
            _ => throw new InvalidOperationException($"Invalid Condition: {Condition}")
        };

        return basePrice * (decimal)conditionMultiplier;
    }
}
