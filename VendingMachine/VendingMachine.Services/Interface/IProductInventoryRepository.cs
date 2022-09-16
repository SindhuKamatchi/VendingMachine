namespace VendingMachine.Services.Interface
{
    public interface IProductInventoryRepository
    {
        Dictionary<string, int> GetInventory();
        Task<Dictionary<string, int>> UpdateInventory(string code);
    }
}
