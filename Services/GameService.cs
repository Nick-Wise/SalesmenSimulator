namespace SalesmenSimulator.Services;

public interface IGameService
{
    GameStartResult StartNewGame(string ownerName, string storeName);
    GameDisplayModel GetGameDisplayModel();
    RestockStatus StartRestock();
    RollStatus RollNewCarBatch();
    RollDisplayModel GetRollDisplayModel();
    BuyStatus BuyCar(int index);
}

internal class GameService : IGameService
{
    private readonly ISessionFactory _sessionFactory;
    private readonly IRestockService _restockService;
    private GameState? _session;
    private protected GameState Session => GetActiveSession();

    public GameService(ISessionFactory sessionFactory, IRestockService restockService)
    {
        _sessionFactory = sessionFactory;
        _restockService = restockService;
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

    public RestockStatus StartRestock() => _restockService.StartRestock(Session);

    public RollStatus RollNewCarBatch() => _restockService.RollNewCarBatch(Session);

    public RollDisplayModel GetRollDisplayModel() => new RollDisplayModel(Session.CurrentBatch, Session.CurrentRerollCost);

    public BuyStatus BuyCar(int index) => _restockService.BuyCar(Session, index);

    private GameState GetActiveSession()
    {
        if (_session is null)
        {
            throw new InvalidOperationException("No active game session.");
        }
        return _session;
    }

}