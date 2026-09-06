namespace SalesmenSimulator.Services;

public interface IGameService
{
    GameStartResult StartNewGame(string ownerName, string storeName);
    GameStatus GetGameStatus();
    void StartRestock();
    RollResult? RollCars();
    void BuyCar(int index);
}

internal class GameService : IGameService
{
    private readonly ISessionFactory _sessionFactory;
    private readonly IRerollService _rerollService;
    private GameSession? _session;
    private protected GameSession Session => GetActiveSession();

    public GameService(ISessionFactory sessionFactory, IRerollService rerollService)
    {
        _sessionFactory = sessionFactory;
        _rerollService = rerollService;
    }
    public GameStartResult StartNewGame(string ownerName, string storeName)
    {
        _session = _sessionFactory.Create(ownerName, storeName);

        return new GameStartResult(_session.Owner.Name, _session.Store.Name);
    }

    public GameStatus GetGameStatus()
    {
        return new GameStatus
        (
            Session.Owner.Balance,
            Session.Store.Cars,
            Session.Store.Capacity
        );
    }

    public void StartRestock()
    {
        Session.ResetRerolls();
    }

    public RollResult? RollCars()
    {
        var rerollCost = Session.CurrentRerollCost;
        var (success, _) = Session.Owner.SpendCash(rerollCost);

        if (!success)
        {
            return null;
        }

        var batch = _rerollService.Roll();
        Session.SetBatch(batch);

        Session.IncrementRerolls();
        decimal nextRerollCost = _rerollService.CalculateRerollCost(Session.RerollsUsed);
        Session.UpdateRerollCost(nextRerollCost);

        return new RollResult(batch, nextRerollCost);
    }

    public void BuyCar(int index)
    {
        var car = Session.CurrentBatch[index];
        if (Session.Store.Cars.Count < Session.Store.Capacity && Session.Owner.Balance >= car.BuyPrice)
        {
            var (success, newBalance) = Session.Owner.SpendCash(car.BuyPrice);
            if (success)
            {
                Session.Store.Cars.Add(car);
            }
        }

    }

    private GameSession GetActiveSession()
    {
        if (_session is null)
        {
            throw new InvalidOperationException("No active game session.");
        }
        return _session;
    }

}