
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

var owner = new Owner(name);
Console.WriteLine($"Hello, {owner.Name}");

Store store = new Store(storeName);
store.BuyCar(CarType.Coupe);


PrintSummary(owner, store);

foreach (Car car in store.Cars)
{
  PrintCarSummary(car);
}


void PrintSummary(Owner owner, Store store)
{
  Console.WriteLine("------- Summary ------- ");
  Console.WriteLine($"Current Balance: {owner.Balance:C}");
  Console.WriteLine($"Lot Capacity: {store.Cars.Count}/{store.Capacity}");

  Console.WriteLine("");
}

void PrintCarSummary(Car car)
{
  Console.WriteLine("------- Car Summary ------- ");
  Console.WriteLine($"Type:{car.Type} | Condition: {car.Condition} | Buy : {car.BuyPrice:C}");
  Console.WriteLine("");
}








