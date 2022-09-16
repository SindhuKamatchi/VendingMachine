using Microsoft.AspNetCore.Mvc;
using VendingMachine.Services.Enum;
using VendingMachine.Services.Interface;
using VendingMachine.Services.Models;

namespace VendingMachine.Services.Services
{
    public class VendingClient : IVendingClient
    {
        private readonly IProductService _productService;
   
        private decimal _cost;

        public VendingClient(IProductService productService)
        {
            if (productService == null) throw new ArgumentNullException("productService parameter is null");

            _productService = productService;
        }
        public async Task<VendingResponse> SelectProduct(string code,decimal amount)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException("Code parameter empty!");

            var response = new VendingResponse();

            //check if the code is valid            
            //if no, return error object with details
            var product = _productService.GetProduct(code);

            //invalid code entered
            if (product == null)
            {
                response.Message = "Invalid Product Selected. Please try again";
                response.IsSuccess = false;
                return response;
            }

            //no coins entered, but selection pressed
            if (amount == 0)
            {
                //if exact change item, message = "exact change only"
                response.Message = "Insert Coin";
                response.IsSuccess = false;
                return response;
            }

            //entered coins less than cost
            if (amount < product.Price)
            {
                response.Message = string.Format(" Credit not enough Price : {0}", product.Price);
                response.IsSuccess = false;
                return response;
            }

            //all good, valid code and valid amount entered
            var quantity = _productService.GetProductQuantity(code);
            if (quantity > 0)
            {
                response.Message = "Thank You";
                response.IsSuccess = true;
                response.inventory = await _productService.UpdateProductQuantity(code);
                response.Change = (Convert.ToDouble(amount - product.Price));
                return response;
            }

            response.Message = "SOLD OUT";
            response.IsSuccess = false;
            return response;
        }
    }
}
