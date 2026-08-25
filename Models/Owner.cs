namespace SalesmenSimulator.Models;

public class Owner(
    string name,
    decimal startingBalance = 10000m,
    int startingPeopleSkills = 1,
    int startingTechnicalSkills = 1)
{

    public string Name => name;

    public decimal Balance
    {
        get;
        private set => field = Math.Max(value, 0);
    } = startingBalance;

    public int PeopleSkills
    {
        get;
        private set => field = Math.Max(value, 1);
    } = startingPeopleSkills;
    public int TechnicalSkills
    {
        get;
        private set => field = Math.Max(value, 1);
    } = startingTechnicalSkills;

    internal decimal DepositCash(decimal amount)
    {
        return Balance += amount;
    }

    internal (bool success, decimal currentBalance) SpendCash(decimal amount)
    {

        if (amount > Balance)
        {
            Console.WriteLine("Insufficient Funds");
            return (false, Balance);
        }

        Balance -= amount;
        return (true, Balance);
    }

    internal int ModifyPeopleSkills(int skills = 0)
    {
        return PeopleSkills += skills;
    }

    internal int ModifyTechnicalSkills(int skills = 0)
    {
        return TechnicalSkills += skills;
    }
}
