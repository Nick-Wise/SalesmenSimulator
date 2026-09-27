using System.Collections.Frozen;
using System.Diagnostics;

namespace SalesmenSimulator.Services;

public interface ICustomerGeneratorService
{
    CustomerDifficulty TestDifficulty();
}

public class CustomerGeneratorService(IRandomProvider randomProvider) : ICustomerGeneratorService
{
    private FrozenDictionary<CustomerDifficulty, int> _difficultyDistro =
    new Dictionary<CustomerDifficulty, int>
    {
        [CustomerDifficulty.Easy] = 5,
        [CustomerDifficulty.Medium] = 9,
        [CustomerDifficulty.Hard] = 1
    }.ToFrozenDictionary();


    public CustomerDifficulty TestDifficulty()
    {
        return ChooseDifficulty();
    }


    private CustomerDifficulty ChooseDifficulty()
    {
        int total = _difficultyDistro.Sum(x => x.Value);
        int roll = randomProvider.Next(total);

        int cumulative = 0;
        foreach (var difficulty in _difficultyDistro)
        {
            cumulative += difficulty.Value;
            if (roll < cumulative)
                return difficulty.Key;
        }

        throw new ArgumentException($"{roll} is Out of difficulty roll range");
    }

}