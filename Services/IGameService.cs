using SalesmenSimulator.Models;

namespace SalesmenSimulator.Services;

public interface IGameService
{
    GameStartResult GameStartSummary(string ownerName, string storeName);
}