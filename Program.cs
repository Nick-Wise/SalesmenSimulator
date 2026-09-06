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


Console.WriteLine("-------- Restock Phase --------");
gameService.StartRestock();
RollResult? rollResult = gameService.RollCars();
bool restocking = true;
while (restocking)
{
  var gameStatus = gameService.GetGameStatus();
  if (gameStatus.Inventory.Count == gameStatus.Capacity)
  {
    Console.WriteLine("Inventory is full.");
    restocking = false;
    break;
  }

  DisplayGameStatus();
  Console.WriteLine("Available Cars:");
  Console.WriteLine("""
   _____________________________________  
  | Index | Type  | Condition |  Price  |
  | ----- | ----- | --------- | ------- |
  """);
  for (int i = 1; rollResult is not null && i <= rollResult.Cars.Count; i++)
  {
    var car = rollResult?.Cars[i - 1];
    Console.WriteLine($"| {i,-5} | {car?.Type,-5} | {car?.Condition,-9} | {car?.BuyPrice,-7} |");
  }
  Console.WriteLine("|_____________________________________|\n");

  Console.WriteLine($"Reroll: ${rollResult?.NextRerollCost ?? 0}");
  Console.WriteLine("To reroll enter 'r'");
  Console.WriteLine("To finish restock enter 'f'");
  Console.WriteLine("To buy car enter index:");
  var input = Console.ReadLine();
  if (input?.ToLower() == "r")
  {
    rollResult = gameService.RollCars();
  }
  else if (input?.ToLower() == "f")
  {
    restocking = false;
  }
  else if (int.TryParse(input, out int index) && index >= 1 && index <= rollResult?.Cars.Count)
  {
    gameService.BuyCar(index - 1);
  }
}

void DisplayGameStatus()
{
  var status = gameService.GetGameStatus();
  Console.WriteLine($"Balance: {status.Balance}");
  Console.WriteLine($"Inventory:");
  foreach (var car in status.Inventory)
  {
    Console.WriteLine($"- Type: {car.Type} Condition: {car.Condition}");
  }
  Console.WriteLine($"Capacity: {status.Inventory.Count}/{status.Capacity}");
}












