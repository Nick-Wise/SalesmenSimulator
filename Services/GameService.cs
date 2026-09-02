namespace SalesmenSimulator.Services;

public interface IGameService
{
    GameStartResult StartNewGame(string ownerName, string storeName);
}

internal class GameService : IGameService
{
    private readonly ISessionFactory _sessionFactory;
    private readonly ICarGeneratorService _carGenerator;
    private GameSession? _session;

    public GameService(ISessionFactory sessionFactory, ICarGeneratorService carGenerator)
    {
        _sessionFactory = sessionFactory;
        _carGenerator = carGenerator;
    }
    public GameStartResult StartNewGame(string ownerName, string storeName)
    {
        _session = _sessionFactory.Create(ownerName, storeName);

        return new GameStartResult(_session.Owner.Name, _session.Store.Name);
    }
}