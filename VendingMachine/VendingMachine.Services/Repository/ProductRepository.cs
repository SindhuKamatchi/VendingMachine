using VendingMachine.Services.Enum;
using VendingMachine.Services.Interface;
using VendingMachine.Services.Models;

namespace VendingMachine.Services.Repository
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products;

        public IEnumerable<Product> GetProductList()
        {
            return _products ?? (_products = new List<Product>
            {
                new Product() {Code = "COLA", Type = ProductItemType.Cola, Name = "Coke", Price = 100},
                new Product() {Code = "CRISP", Type = ProductItemType.Crisp, Name = "Crisp", Price = 50},
                new Product() {Code = "CHOCLATE", Type = ProductItemType.Choclate, Name = "Choclate", Price = 65}
            });
        }
    }
}
