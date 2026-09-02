namespace SalesmenSimulator.Services;

public interface ICarGeneratorService
{
    public Car GenerateCar();
}

public class CarGeneratorService : ICarGeneratorService
{
    private readonly IRandomProvider _randomProvider;

    public CarGeneratorService(IRandomProvider randomProvider)
    {
        _randomProvider = randomProvider;
    }
    public Car GenerateCar()
    {
        return new Car(RollCarType(), RollCarCondition());
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

    private CarType RollCarType()
    {
        int randomNum = _randomProvider.Next(3);
        return randomNum switch
        {
            0 => CarType.Sedan,
            1 => CarType.Coupe,
            2 => CarType.Truck,
            3 => CarType.Suv,
            _ => throw new ArgumentException($"Invalid {nameof(CarType)}: {randomNum}")
        };
    }
}
