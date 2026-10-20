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
    private RestockState? _restockState;
    private protected GameState GameState => GetActiveGameState();
    private protected RestockState RestockState => EnsureRestockState();

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

    public RestockStatus StartRestock()
    {
        if (GameState.Store.IsFull())
        {
            return RestockStatus.InventoryFull;
        }

        _restockState = new RestockState();

       return _restockService.StartRestock(RestockState);
    }

    public RollStatus RollNewCarBatch()
    {
        WithdrawResult result = GameState.Owner.TryWithdrawCash(RestockState.RerollCost);

        if (!result.Success)
        {
            return RollStatus.InsufficientFunds;
        }

        return _restockService.RollNewCarBatch(RestockState);
    }

    public RollDisplayModel GetRollDisplayModel()
    {
        if(RestockState is null)
        {
            throw new InvalidOperationException("Restock process has not been started.");
        }

        return new RollDisplayModel(RestockState.CurrentBatch, RestockState.RerollCost);
    }

    public BuyStatus BuyCar(int index)
    {
        if (index < 0 || index >= RestockState.BatchSize)
        {
            return BuyStatus.InvalidIndex;
        }
        Car carToBuy = RestockState.CurrentBatch[index];
        WithdrawResult result = GameState.Owner.TryWithdrawCash(carToBuy.BuyPrice);
        if (!result.Success)
        {
            return BuyStatus.InsufficientFunds;
        }
        GameState.Store.Inventory.Add(carToBuy);
        RestockState.RemoveCarFromBatch(index);
        return BuyStatus.Success;
    }

    private RestockState EnsureRestockState()
    {
        if (_restockState is null)
        {
            throw new InvalidOperationException("Restock process has not been started.");
        }
        return _restockState;
    }

    private GameState GetActiveGameState()
    {
        if (_gameState is null)
        {
            throw new InvalidOperationException("No active game session.");
        }
        return _gameState;
    }

}