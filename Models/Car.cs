namespace SalesmenSimulator.Models;

public class Car(CarType type, CarCondition condition, decimal buyPrice)
{
    public CarType Type { get; } = type;
    public CarCondition Condition { get; } = condition;
    public decimal BuyPrice { get; } = buyPrice;

    public decimal SellPrice { get; set; }
}
