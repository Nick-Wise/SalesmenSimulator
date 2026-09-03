namespace SalesmenSimulator.Models;

public class GameSession(Owner owner, Store store)
{
    public Owner Owner => owner;
    public Store Store => store;
    public int RerollsUsed { get; private set; }
    public int Day { get; private set; }
    public void IncrementRerolls() => RerollsUsed++;
    public void NextDay() => Day++;
}
