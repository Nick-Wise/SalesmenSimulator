namespace SalesmenSimulator.Services;

public interface ISessionFactory
{
    GameSession Create(string ownerName, string storeName);
    GameSession Load();
}

public class SessionFactory : ISessionFactory
{
    public GameSession Create(string ownerName, string storeName)
    {
        return new GameSession(new Owner(ownerName), new Store(storeName));
    }

    public GameSession Load()
    {
        throw new NotImplementedException();
    }
}
