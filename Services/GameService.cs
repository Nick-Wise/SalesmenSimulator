namespace SalesmenSimulator.Services;

internal class GameService : IGameService
{
    private readonly ISessionFactory _sessionFactory;
    private GameSession? _session;

    public GameService(ISessionFactory sessionFactory)
    {
        _sessionFactory = sessionFactory;
    }
    public GameStartResult StartNewGame(string ownerName, string storeName)
    {
        _session = _sessionFactory.Create(ownerName, storeName);

        return new GameStartResult
        {
            OwnerName = _session.Owner.Name,
            StoreName = _session.Store.Name
        };
    }
}