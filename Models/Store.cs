namespace SalesmenSimulator.obj.Models;

internal class Store
{
    public int MaxRating => Tier switch
    {
        1 => 60,
        2 => 70,
        3 => 80,
        4 => 100,
        _ => throw new ArgumentOutOfRangeException(nameof(Tier), $"Invalid Tier: {Tier}")
    };

    public int MaxSize => Tier switch
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
        set => field = Math.Clamp(value, 1, 4);
    }

    public int Rating
    {
        get;
        set => field = Math.Clamp(value, 0, MaxRating);
    }

    public int Size
    {
        get;
        set => field = Math.Clamp(value, 5, MaxSize);
    }

    //List of Cars





}
