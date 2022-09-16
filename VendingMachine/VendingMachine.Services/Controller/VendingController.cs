using Microsoft.AspNetCore.Mvc;
using VendingMachine.Services.Interface;
using VendingMachine.Services.Models;

namespace VendingMachine.Services.Controller
{
    [ApiController]
    [Route("v1/vending")]
    public class VendingController
    {
        private readonly ILogger<VendingController> _logger;
        private readonly IVendingClient _client;

        public VendingController(ILogger<VendingController> logger, IVendingClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet("code={code}&amount={amount}")]
        public  async Task<ActionResult<VendingResponse>> GetProduct(string code, decimal amount)
        {
            VendingResponse response = new VendingResponse();
            try
            {
                _logger.LogInformation("Request received {code}", code);
                  response=  await _client.SelectProduct(code, amount);
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Calling{code} at {date}", code ,DateTime.Now);
                 response = new VendingResponse
                {
                    Message = "Error",
                    IsSuccess = false
                };
                
            }
            return response;
        }

    }
}
