using VendingMachine.Services.Models;

namespace VendingMachine.Services.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProductList();
    }
}
