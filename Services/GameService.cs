namespace SalesmenSimulator.Services;

internal class GameService : IGameService
{
    private Owner _owner;
    private Store _store;

    private GameService(Owner owner, Store store)
    {
        _owner = owner;
        _store = store;
    }

    public static GameService StartNewGame(string ownerName, string storeName)
    {
        var owner = new Owner(ownerName);
        var store = new Store(storeName);

        return new GameService(owner, store);
    }


    public GameStartResult GameStartSummary(string ownerName, string storeName)
    {
        return new GameStartResult
        {
            OwnerName = _owner.Name,
            StoreName = _store.Name
        };
    }
}