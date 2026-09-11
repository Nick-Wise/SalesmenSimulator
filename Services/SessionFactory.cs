namespace SalesmenSimulator.Services;

public interface ISessionFactory
{
    GameState Create(string ownerName, string storeName);
    GameState Load();
}

public class SessionFactory : ISessionFactory
{
    public GameState Create(string ownerName, string storeName)
    {
        return new GameState(new Owner(ownerName), new Store(storeName));
    }

    public GameState Load()
    {
        throw new NotImplementedException();
    }
}
