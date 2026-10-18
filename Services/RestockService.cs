using static System.Collections.Specialized.BitVector32;

namespace SalesmenSimulator.Services;

public interface IRestockService
{
    public RestockStatus StartRestock(RestockState restockState);
    public RollStatus RollNewCarBatch(GameState gameState);
    public BuyStatus BuyCar(GameState gameState, int index);
}

public class RestockService(ICarGeneratorService carGenerator, IRestockState restockState) : IRestockService
{
    private const int _batchSize = 3;
    
   
    public RestockStatus StartRestock(RestockState restockState)
    {
        restockState.ResetRerolls();
        List<Car> batch = RollBatch();
        restockState.SetBatch(batch);
        return RestockStatus.Success;
    }

    public RollStatus RollNewCarBatch(GameState gameState)
    {
        WithdrawResult result = gameState.Owner.TryWithdrawCash(restockState.RerollCost);

        if (!result.Success)
        {
            return RollStatus.InsufficientFunds;
        }

        var batch = RollBatch();
        restockState.SetBatch(batch);

        restockState.IncrementRerolls();

        return RollStatus.Success;
    }

    public BuyStatus BuyCar(GameState gameState, int index)
    {
        if (index < 0 || index >= _batchSize)
        {
            return BuyStatus.InvalidIndex;
        }
        Car carToBuy = restockState.CurrentBatch[index];
        WithdrawResult result = gameState.Owner.TryWithdrawCash(carToBuy.BuyPrice);
        if (!result.Success)
        {
            return BuyStatus.InsufficientFunds;
        }
        gameState.Store.Inventory.Add(carToBuy);
        restockState.RemoveCarFromBatch(index);
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

   
}
