using SalesmenSimulator.Models;

string? name = "";

while (string.IsNullOrEmpty(name))
{
  Console.Write("Enter your Name: ");
  name = Console.ReadLine();
}

var owner = new Owner(name);

Console.WriteLine($"Hello, {owner.Name}");
Console.WriteLine($"Current Balance: {owner.Balance:C}");
Console.WriteLine($"People Skills: {owner.PeopleSkills} Technical Skills: {owner.TechnicalSkills}");




