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
var result = gameService.GameStartSummary(name, storeName);











