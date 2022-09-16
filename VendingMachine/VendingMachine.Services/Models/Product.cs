using VendingMachine.Services.Enum;

namespace VendingMachine.Services.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public ProductItemType Type { get; set; }
    }
}
