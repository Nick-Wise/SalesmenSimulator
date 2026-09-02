namespace SalesmenSimulator.Services;

public class SystemRandomProvider() : IRandomProvider
{
    public int Next(int max) => Random.Shared.Next(max);
}