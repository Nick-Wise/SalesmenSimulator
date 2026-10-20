namespace SalesmenSimulator.Models;

public interface IRestockState
{
    int RerollsUsed { get; }
    decimal RerollCost { get; }
    IReadOnlyList<Car> CurrentBatch { get; }
    void ResetRerolls();
    void IncrementRerolls();
    void SetBatch(IEnumerable<Car> batch);
    void RemoveCarFromBatch(int index); 
}

public class RestockState : IRestockState
{
    private List<Car> _currentBatch = [];
    private const decimal BasePrice = 100;
    public int RerollsUsed { get; private set; }
    public decimal RerollCost => CalculateRerollCost(RerollsUsed);
    public IReadOnlyList<Car> CurrentBatch => _currentBatch;

    public const int BatchSize = 3;

    public void ResetRerolls()
    {
        RerollsUsed = 0;
    }
    public void IncrementRerolls() => RerollsUsed++;

    public void SetBatch(IEnumerable<Car> batch) => _currentBatch = batch.ToList();
    public void RemoveCarFromBatch(int index)
    {
        if (index < 0 || index >= _currentBatch.Count())
        {
            throw new ArgumentOutOfRangeException("invalid batch index");
        }
        _currentBatch.RemoveAt(index);
    }

    private decimal CalculateRerollCost(int numberOfRerolls) => BasePrice * (numberOfRerolls + 1);
}
