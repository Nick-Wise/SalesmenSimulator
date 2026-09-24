namespace SalesmenSimulator.Models;



public class Customer(CustomerDifficulty difficulty, CarType preferredCarType, BudgetRange budgetRange, int offerAttempts, decimal trueBudget)
{


    public CustomerDifficulty Difficulty = difficulty;
    public CarType PreferredCarType = preferredCarType;
    public BudgetRange BudgetRange = budgetRange;
    public decimal TrueBudget = trueBudget;
    public int OfferAttempts = offerAttempts;
}
