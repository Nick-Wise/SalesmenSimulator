using static System.Collections.Specialized.BitVector32;

namespace SalesmenSimulator.Services;

public interface IRestockService
{
    public RestockStatus StartRestock(RestockState restockState);
    public RollStatus RollNewCarBatch(RestockState restockState);

}

public class RestockService(ICarGeneratorService carGenerator, IRestockState restockState) : IRestockService
{
    public RestockStatus StartRestock(RestockState restockState)
    {
        restockState.ResetRerolls();
        List<Car> batch = RollBatch();
        restockState.SetBatch(batch);
        return RestockStatus.Success;
    }

    public RollStatus RollNewCarBatch(RestockState restockState)
    {
        var batch = RollBatch();
        restockState.SetBatch(batch);

        restockState.IncrementRerolls();

        return RollStatus.Success;
    }

    private List<Car> RollBatch()
    {
        var batch = new List<Car>();
        for (int i = 0; i < RestockState.BatchSize; i++)
        {
            batch.Add(carGenerator.GenerateCar());
        }
        return batch;
    }

   
}
