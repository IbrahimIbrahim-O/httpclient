using httpclient.Models;
using httpclient.Service;
using httpclient.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace httpclient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFinnhubService _finhubservice;
        private readonly IOptions<OptionsClass> _options;
       
        public HomeController(IFinnhubService finhubservice, IOptions<OptionsClass> options)
        {
            _finhubservice = finhubservice;
            _options = options;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            Dictionary<string, object>? model = await _finhubservice.GetStockPriceQuote
                (_options.Value.DefaultParameter);

            var stock = new Stocks
            {
                StockSymbol = _options.Value.DefaultParameter,
                CurrentPrice = Convert.ToDouble(model["c"].ToString()),
                HighestPrice = Convert.ToDouble(model["h"].ToString()),
                LowestPrie = Convert.ToDouble(model["l"].ToString()),
                OpenPrice = Convert.ToDouble(model["o"].ToString())
            };

            return View(stock);
        }
    }
}
