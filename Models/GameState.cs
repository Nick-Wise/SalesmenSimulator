namespace SalesmenSimulator.Models;

/// <summary>
/// Owns real game state and mutation. Never Expose Directly to
/// the display layer.
/// </summary>
/// <param name="owner"></param>
/// <param name="store"></param>
public class GameState(Owner owner, Store store)
{
    
    public Owner Owner => owner;
    public Store Store => store;
    public int Day { get; private set; }
    
    public void NextDay() => Day++;
}
