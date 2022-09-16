using VendingMachine.Services.Interface;
using VendingMachine.Services.Models;

namespace VendingMachine.Services.Services
{
    public class ProductClient : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IProductInventoryRepository _productInventoryRepository;
        public ProductClient(IProductRepository productRepository, IProductInventoryRepository productInventoryRepository)
        {
            if (productRepository == null) throw new ArgumentNullException("productRepository parameter is null");
            if (productInventoryRepository == null) throw new ArgumentNullException("productInventoryRepository parameter is null");

            _productRepository = productRepository;
            _productInventoryRepository = productInventoryRepository;
        }

        public int GetProductQuantity(string code)
        {
            var quantities = _productInventoryRepository.GetInventory();
            return quantities.Count(x => x.Key == code.ToUpper());
            //return quantities[code];
        }

        public Product GetProduct(string code)
        {
            return GetAllProducts().FirstOrDefault(x => x.Code == code.ToUpper());
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetProductList();
        }

        public async Task<Dictionary<string, int>> UpdateProductQuantity(string code)
        {

             Dictionary<string, int> _productQuantities= await  _productInventoryRepository.UpdateInventory(code);
            return _productQuantities;
        }
    }
}
