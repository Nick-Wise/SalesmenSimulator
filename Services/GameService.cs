namespace SalesmenSimulator.Services;

public interface IGameService
{
    GameStartResult StartNewGame(string ownerName, string storeName);
    GameStatus GetGameStatus();
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

    private GameSession GetActiveSession()
    {
        if (_session is null)
        {
            throw new InvalidOperationException("No active game session.");
        }
        return _session;
    }

}