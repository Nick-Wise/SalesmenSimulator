namespace SalesmenSimulator.Services;

public interface ICarCatalog
{
    decimal GetBasePrice(CarType carType);
}

public class CarCatalog : ICarCatalog
{
    private readonly IReadOnlyDictionary<CarType, decimal> CarBasePrices =
    new Dictionary<CarType, decimal>
    {
        [CarType.Coupe] = 35000,
        [CarType.Suv] = 20000,
        [CarType.Truck] = 15000,
        [CarType.Sedan] = 10000,
    };

    public decimal GetBasePrice(CarType carType)
    {
        return CarBasePrices[carType];
    }
}


