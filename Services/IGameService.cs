using SalesmenSimulator.Models;

namespace SalesmenSimulator.Services;

public interface IGameService
{
    GameStartResult GameSetup(string ownerName, string storeName);
}