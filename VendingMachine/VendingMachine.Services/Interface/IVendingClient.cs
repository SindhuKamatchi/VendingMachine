using VendingMachine.Services.Models;

namespace VendingMachine.Services.Interface
{
    public interface IVendingClient
    {

        Task<VendingResponse> SelectProduct(string code,decimal amount);
       
    }
}
