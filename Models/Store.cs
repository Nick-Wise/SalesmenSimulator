namespace SalesmenSimulator.Models;

public class Store(
    string name,
    int startingTier = 1,
    int startingRating = 40,
    int startingCapacity = 5
)
{

    public string Name => name;
    private int MaxRating => Tier switch
    {
        1 => 60,
        2 => 70,
        3 => 80,
        4 => 100,
        _ => throw new ArgumentOutOfRangeException(nameof(Tier), $"Invalid Tier: {Tier}")
    };

    private int MaxCapacity => Tier switch
    {
        1 => 5,
        2 => 10,
        3 => 15,
        4 => 20,
        _ => throw new ArgumentOutOfRangeException(nameof(Tier), $"Invalid Tier: {Tier}")
    };

    public int Tier
    {
        get;
        private set => field = Math.Clamp(value, 1, 4);
    } = startingTier;

    public int Rating
    {
        get;
        private set => field = Math.Clamp(value, 0, MaxRating);
    } = startingRating;

    public int Capacity
    {
        get;
        private set => field = Math.Clamp(value, 5, MaxCapacity);
    } = startingCapacity;

    public List<Car> Cars { get; private set; } = [];
}
