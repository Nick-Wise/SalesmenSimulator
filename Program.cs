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

#region Restock Phase
Console.WriteLine("-------- Restock Phase --------");
RollStatus rollStatus = gameService.StartRestock();

bool restocking = true;
while (restocking)
{
  GameDisplayModel session = gameService.GetGameDisplayModel();
  DisplayGameInfo();



  Console.WriteLine("Available Cars:");
  RollDisplayModel rollResult = gameService.GetRollDisplayModel();
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
    rollStatus = gameService.RollCars();
    if (rollStatus is RollStatus.InsufficientFunds)
    {
      Console.ForegroundColor = ConsoleColor.DarkYellow;
      Console.WriteLine("Not enough money to reroll");
      Console.ResetColor();
    }
  }
  else if (input?.ToLower() == "f")
  {
    restocking = false;
  }
  else if (int.TryParse(input, out int index) && index >= 1 && index <= rollResult?.Cars.Count)
  {
    BuyStatus buyStatus = gameService.BuyCar(index - 1);
    if (buyStatus is BuyStatus.InsufficientFunds)
    {
      Console.ForegroundColor = ConsoleColor.DarkYellow;
      Console.WriteLine("Not enough money to buy that car");
      Console.ResetColor();
    }

    if (buyStatus is BuyStatus.InventoryFull)
    {
      Console.ForegroundColor = ConsoleColor.DarkYellow;
      Console.WriteLine("Inventory Full");
      Console.ResetColor();
    }
  }
}
#endregion RestockPhase

void DisplayGameInfo()
{
  GameDisplayModel info = gameService.GetGameDisplayModel();
  Console.ForegroundColor = ConsoleColor.Green;
  Console.WriteLine($"Balance: {info.Balance:C}");
  Console.ResetColor();
  Console.WriteLine($"Inventory:");
  foreach (var car in info.Inventory)
  {
    Console.WriteLine($"- Type: {car.Type} Condition: {car.Condition}");
  }
  Console.WriteLine($"Capacity: {info.Inventory.Count}/{info.Capacity}");
}












