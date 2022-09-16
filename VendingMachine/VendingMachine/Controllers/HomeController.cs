using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using VendingMachine.Models;
using VendingMachine.Models.ConfigData;
using VendingMachine.Services.Models;
using VendingMachine.Services.Services;

namespace VendingMachine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient httpClient;
        private readonly VendingServiceSettings settings;
        private readonly VendingClient _vendingService;
        private readonly IConfiguration config;

        public HomeController(ILogger<HomeController> logger, IOptions<VendingServiceSettings> options,IConfiguration configuration)
        {
            _logger = logger;
            settings = options.Value;
            config = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Index(VendingModel vendingModel)
        {
            try
            {
                int amount = vendingModel.Amount;
                string code = vendingModel.code;
                if(amount==0)
                {
                    ViewBag.SucessMessage = "API Response" + "-" + "INSERT COIN";
                }
                else if(string.IsNullOrEmpty(code))
                {
                    ViewBag.SucessMessage = "API Response" + "-" + "Please choose product";
                }
                else
                {
                    using (var httpClient = new HttpClient())
                    {
                        var url = config.GetSection("VendingServiceSettings").GetSection("url").Value;
                        url = $"{url}code={code}&amount={amount}";  //string.Format("https://localhost:7069/v1/vending/code={0}a&amount={1}", code, amount);
                        using (var response = await httpClient.GetAsync(url))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            vendingModel.VendingResponse = apiResponse;
                            Models.VendingResponse vendingResponse = JsonConvert.DeserializeObject<Models.VendingResponse>(apiResponse);
                            ViewBag.SucessMessage = "API Response" + "-" + apiResponse;
                        }
                    }
                }
                
                return View(vendingModel);
            }
            catch(Exception ex)
            {
               throw ex;
            }
        }
        public IActionResult Index()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}