using SalesmenSimulator.Models;

namespace SalesmenSimulator.Services;

public interface IGameService
{
    GameStartResult StartNewGame(string ownerName, string storeName);
}