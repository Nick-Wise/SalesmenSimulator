namespace SalesmenSimulator.Services;

public interface ICarGeneratorService
{
    public Car GenerateCar();
}

public class CarGeneratorService : ICarGeneratorService
{
    private readonly IRandomProvider _randomProvider;
    private readonly ICarCatalog _carCatalog;

    public CarGeneratorService(IRandomProvider randomProvider, ICarCatalog carCatalog)
    {
        _randomProvider = randomProvider;
        _carCatalog = carCatalog;
    }
    public Car GenerateCar()
    {
        CarType carType = RollCarType();
        CarCondition carCondition = RollCarCondition();
        decimal buyPrice = CalculateBuyPrice(carType, carCondition);

        return new Car(carType, carCondition, buyPrice);
    }

    private CarType RollCarType()
    {
        int randomNum = _randomProvider.Next(Enum.GetValues<CarType>().Count());
        return randomNum switch
        {
            0 => CarType.Sedan,
            1 => CarType.Coupe,
            2 => CarType.Truck,
            3 => CarType.Suv,
            _ => throw new ArgumentException($"Invalid {nameof(CarType)}: {randomNum}")
        };
    }
    private CarCondition RollCarCondition()
    {
        int randomNum = _randomProvider.Next(100);
        return randomNum switch
        {
            < 10 => CarCondition.D,
            < 35 => CarCondition.C,
            < 65 => CarCondition.B,
            < 90 => CarCondition.A,
            _ => CarCondition.S,

        };
    }
    private decimal CalculateBuyPrice(CarType carType, CarCondition carCondition)
    {
        decimal basePrice = _carCatalog.GetBasePrice(carType);

        decimal conditionMultiplier = carCondition switch
        {
            CarCondition.D => 0.6m,
            CarCondition.C => 0.8m,
            CarCondition.B => 1.0m,
            CarCondition.A => 1.2m,
            CarCondition.S => 1.4m,
            _ => throw new InvalidOperationException($"Invalid Condition: {carCondition}")
        };

        return basePrice * conditionMultiplier;
    }

}
