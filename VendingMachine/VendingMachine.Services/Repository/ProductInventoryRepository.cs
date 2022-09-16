using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using VendingMachine.Services.Interface;

namespace VendingMachine.Services.Repository
{
    public class ProductInventoryRepository : IProductInventoryRepository
    {
        public static Dictionary<string, int> _productQuantities;
        public Dictionary<string, int> GetInventory()
        {
            return _productQuantities ?? (_productQuantities = new Dictionary<string, int> { 
                { "COLA", 20 }, 
                { "CRISP", 15 }, 
                { "CHOCLATE", 10 }});
        }
        public async Task<Dictionary<string, int>> UpdateInventory(string code)
        {
            var convertedDictionary = _productQuantities.ToDictionary(item => item.Key.ToString(), item => item.Value.ToString()); //This converts your dictionary to have the Key and Value of type string
            var json = JsonConvert.SerializeObject(convertedDictionary);
            var currentCount =    _productQuantities[code.ToUpper()];
            if (currentCount > 0)
                _productQuantities[code.ToUpper()]--;
            return _productQuantities;
        }
    }
}
