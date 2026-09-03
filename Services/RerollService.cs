namespace SalesmenSimulator.Services;

public interface IRerollService
{
    public List<Car> Reroll();
}

public class RerollService : IRerollService
{
    private const int _batchSize = 3;
    private const decimal BasePrice = 100;
    private readonly ICarGeneratorService _carGenerator;
    public RerollService(ICarGeneratorService carGenerator)
    {
        _carGenerator = carGenerator;
    }
    public List<Car> Reroll()
    {
        var batch = new List<Car>();
        for (int i = 0; i < _batchSize; i++)
        {
            batch.Add(_carGenerator.GenerateCar());
        }

        return batch;
    }

    public decimal CalculateRerollCost(int numberOfRerolls) => BasePrice * numberOfRerolls;
}
