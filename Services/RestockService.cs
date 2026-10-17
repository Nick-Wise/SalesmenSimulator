using static System.Collections.Specialized.BitVector32;

namespace SalesmenSimulator.Services;

public interface IRestockService
{
    public RestockStatus StartRestock(GameState gameState);
    public RollStatus RollNewCarBatch(GameState gameState);
    public BuyStatus BuyCar(GameState gameState, int index);
}

public class RestockService(ICarGeneratorService carGenerator) : IRestockService
{
    private const int _batchSize = 3;
    private const decimal BasePrice = 100;
   
    public RestockStatus StartRestock(GameState gameState)
    {
        if (gameState.Store.IsFull())
        {
            return RestockStatus.InventoryFull;
        }
        gameState.ResetRerolls();
        List<Car> batch = RollBatch();
        gameState.SetBatch(batch);
        gameState.UpdateRerollCost(CalculateRerollCost(gameState.RerollsUsed));
        return RestockStatus.Success;
    }

    public RollStatus RollNewCarBatch(GameState gameState)
    {
        decimal rerollCost = gameState.CurrentRerollCost;
        WithdrawResult result = gameState.Owner.TryWithdrawCash(rerollCost);

        if (!result.Success)
        {
            return RollStatus.InsufficientFunds;
        }

        var batch = RollBatch();
        gameState.SetBatch(batch);

        gameState.IncrementRerolls();
        decimal nextRerollCost = CalculateRerollCost(gameState.RerollsUsed);
        gameState.UpdateRerollCost(nextRerollCost);

        return RollStatus.Success;
    }

    public BuyStatus BuyCar(GameState gameState, int index)
    {
        if (index < 0 || index >= _batchSize)
        {
            return BuyStatus.InvalidIndex;
        }
        Car carToBuy = gameState.CurrentBatch[index];
        WithdrawResult result = gameState.Owner.TryWithdrawCash(carToBuy.BuyPrice);
        if (!result.Success)
        {
            return BuyStatus.InsufficientFunds;
        }
        gameState.Store.Inventory.Add(carToBuy);
        gameState.RemoveCarFromBatch(index);
        return BuyStatus.Success;
    }

    private List<Car> RollBatch()
    {
        var batch = new List<Car>();
        for (int i = 0; i < _batchSize; i++)
        {
            batch.Add(carGenerator.GenerateCar());
        }
        return batch;
    }

    private decimal CalculateRerollCost(int numberOfRerolls) => BasePrice * (numberOfRerolls + 1);
}
