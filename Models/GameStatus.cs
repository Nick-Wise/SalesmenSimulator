namespace SalesmenSimulator.Models;
using System.Collections.Generic;
/// <summary>
/// Read-Only display snapshot. No methods, no mutation,
/// no computed properties.
/// </summary>
public class GameStatus(decimal balance, IReadOnlyList<Car> inventory, int capacity)
{
    public decimal Balance => balance;
    public IReadOnlyList<Car> Inventory => inventory;
    public int Capacity => capacity;
}
