using VendingMachine.Services.Models;

namespace VendingMachine.Services.Interface
{
    public interface IProductService
    {
        int GetProductQuantity(string code);
        Product GetProduct(string code);
        IEnumerable<Product> GetAllProducts();
        Task<Dictionary<string, int>> UpdateProductQuantity(string code);
    }
}
