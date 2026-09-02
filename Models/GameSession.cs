namespace SalesmenSimulator.Models;

public class GameSession(Owner owner, Store store)
{
    public Owner Owner => owner;
    public Store Store => store;
    public int Day { get; private set; }

    public void NextDay()
    {
        Day++;
    }
}
