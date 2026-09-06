using Microsoft.Extensions.DependencyInjection;
using SalesmenSimulator.Services;

var services = new ServiceCollection();
services.AddGameServices();

using var provider = services.BuildServiceProvider();

string? name = "";
string? storeName = "";

while (string.IsNullOrEmpty(name))
{
  Console.Write("Enter your name: ");
  name = Console.ReadLine();
}

while (string.IsNullOrWhiteSpace(storeName))
{

  Console.Write("Enter your store name: ");
  storeName = Console.ReadLine();
}

var gameService = provider.GetRequiredService<IGameService>();
var result = gameService.StartNewGame(name, storeName);

Console.WriteLine($"Hi {result.OwnerName}, your the new owner of {result.StoreName}");
DisplayGameStatus();


void DisplayGameStatus()
{
  var status = gameService.GetGameStatus();
  Console.WriteLine($"Balance: {status.Balance}");
  Console.WriteLine($"Inventory: {(status.Inventory.Count > 0 ? string.Join(", ", status.Inventory) : "Empty")}");
  Console.WriteLine($"Capacity: {status.Inventory.Count}/{status.Capacity}");
}












