namespace SalesmenSimulator.Helpers;

public static class PhaseHelpers
{
    public static bool InitializeRestock(RestockStatus status)
    {
        switch (status)
        {
            case RestockStatus.InventoryFull:
                Console.WriteLine("Inventory full, skipping restock phase");
                return false;
            case RestockStatus.Success:
                Console.WriteLine("-------- Restock Phase --------");
                return true;
            default:
                throw new InvalidOperationException($"Unhandled RestockStatus: {status}");
        }
    }
}
