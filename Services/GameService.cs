namespace SalesmenSimulator.Services;

public interface IGameService
{
    GameStartResult StartNewGame(string ownerName, string storeName);
    GameDisplayModel GetGameDisplayModel();
    RollStatus StartRestock();
    RollStatus RollCars();
    RollDisplayModel GetRollDisplayModel();
    BuyStatus BuyCar(int index);
}

internal class GameService : IGameService
{
    private readonly ISessionFactory _sessionFactory;
    private readonly IRerollService _rerollService;
    private GameState? _session;
    private protected GameState Session => GetActiveSession();

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

    public GameDisplayModel GetGameDisplayModel()
    {
        return new GameDisplayModel
        (
            Session.Owner.Balance,
            Session.Store.Inventory,
            Session.Store.Capacity
        );
    }

    public RollStatus StartRestock()
    {
        Session.ResetRerolls();
        List<Car> batch = _rerollService.Roll();
        Session.SetBatch(batch);
        Session.UpdateRerollCost(_rerollService.CalculateRerollCost(Session.RerollsUsed));
        return RollStatus.Success;
    }

    public RollStatus RollCars()
    {
        decimal rerollCost = Session.CurrentRerollCost;
        WithdrawResult result = Session.Owner.TryWithdrawCash(rerollCost);

        if (!result.Success)
        {
            return RollStatus.InsufficientFunds;
        }

        var batch = _rerollService.Roll();
        Session.SetBatch(batch);

        Session.IncrementRerolls();
        decimal nextRerollCost = _rerollService.CalculateRerollCost(Session.RerollsUsed);
        Session.UpdateRerollCost(nextRerollCost);

        return RollStatus.Success;
    }

    public RollDisplayModel GetRollDisplayModel() => new RollDisplayModel(Session.CurrentBatch, Session.CurrentRerollCost);

    public BuyStatus BuyCar(int index)
    {
        var car = Session.CurrentBatch[index];

        if (Session.Store.Inventory.Count >= Session.Store.Capacity)
        {
            return BuyStatus.InventoryFull;
        }

        WithdrawResult result = Session.Owner.TryWithdrawCash(car.BuyPrice);

        if (!result.Success)
        {
            return BuyStatus.InsufficientFunds;
        }

        Session.Store.Inventory.Add(car);
        Session.RemoveFromBatch(index);
        return BuyStatus.Success;
    }

    private GameState GetActiveSession()
    {
        if (_session is null)
        {
            throw new InvalidOperationException("No active game session.");
        }
        return _session;
    }

}