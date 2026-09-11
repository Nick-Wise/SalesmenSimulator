namespace SalesmenSimulator.Models;

public class RollDisplayModel(IReadOnlyList<Car> cars, decimal nextRerollCost)
{
    public IReadOnlyList<Car> Cars => cars;
    public decimal NextRerollCost => nextRerollCost;
}