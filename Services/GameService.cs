namespace SalesmenSimulator.Services;

public interface IGameService
{
    GameStartResult StartNewGame(string ownerName, string storeName);
}

internal class GameService : IGameService
{
    private readonly ISessionFactory _sessionFactory;
    private readonly IRerollService _rerollService;
    private GameSession? _session;

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

    public RestockResult StartRestock()
    {

    }

    private bool SessionExists()
    {
        if (_session is null) return false;
        else return true;
    }
}