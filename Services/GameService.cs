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
    private GameState? _gameState;
    private protected GameState GameState => GetActiveGameState();

    public GameService(ISessionFactory sessionFactory, IRestockService restockService)
    {
        _sessionFactory = sessionFactory;
        _restockService = restockService;
    }
    public GameStartResult StartNewGame(string ownerName, string storeName)
    {
        _gameState = _sessionFactory.Create(ownerName, storeName);

        return new GameStartResult(_gameState.Owner.Name, _gameState.Store.Name);
    }

    public GameDisplayModel GetGameDisplayModel()
    {
        return new GameDisplayModel
        (
            GameState.Owner.Balance,
            GameState.Store.Inventory,
            GameState.Store.Capacity
        );
    }

    public RestockStatus StartRestock() => _restockService.StartRestock(GameState);

    public RollStatus RollNewCarBatch() => _restockService.RollNewCarBatch(GameState);

    public RollDisplayModel GetRollDisplayModel() => new RollDisplayModel(GameState.CurrentBatch, GameState.CurrentRerollCost);

    public BuyStatus BuyCar(int index) => _restockService.BuyCar(GameState, index);

    private GameState GetActiveGameState()
    {
        if (_gameState is null)
        {
            throw new InvalidOperationException("No active game session.");
        }
        return _gameState;
    }

}