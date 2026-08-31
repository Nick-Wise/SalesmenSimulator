namespace SalesmenSimulator.Services;

public interface ISessionFactory
{
    GameSession Create(string ownerName, string storeName);
    GameSession Load();
}
