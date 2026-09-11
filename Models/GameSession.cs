namespace SalesmenSimulator.Models;

/// <summary>
/// Owns real game state and mutation. Never Expose Directly to
/// the display layer.
/// </summary>
/// <param name="owner"></param>
/// <param name="store"></param>
public class GameState(Owner owner, Store store)
{
    private List<Car> _currentBatch = [];
    public Owner Owner => owner;
    public Store Store => store;
    public int RerollsUsed { get; private set; }
    public decimal CurrentRerollCost { get; private set; }
    public IReadOnlyList<Car> CurrentBatch => _currentBatch;
    public int Day { get; private set; }
    public void ResetRerolls()
    {
        RerollsUsed = 0;
        CurrentRerollCost = 0m;
    }
    public void IncrementRerolls() => RerollsUsed++;
    public void UpdateRerollCost(decimal cost)
    {
        if (cost < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cost), "Initial cost should not be negative");
        }

        if (CurrentRerollCost < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cost), "Reroll Cost should not be 0");
        }

        CurrentRerollCost = cost;
    }

    public void SetBatch(IEnumerable<Car> batch) => _currentBatch = batch.ToList();
    public void RemoveFromBatch(int index)
    {
        if (index < 0 || index >= _currentBatch.Count())
        {
            throw new ArgumentOutOfRangeException("invalid batch index");
        }
        _currentBatch.RemoveAt(index);
    }

    public void NextDay() => Day++;
}
