using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Abstraction.ModelBuilders;

namespace WeatherApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IWeatherResultViewModelBuilder _weatherResultViewModelBuilder;

		public HomeController(IWeatherResultViewModelBuilder weatherResultViewModelBuilder)
		{
			_weatherResultViewModelBuilder = weatherResultViewModelBuilder;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("search")]
		public async Task<IActionResult> Search([FromQuery(Name = "q")]string location)
		{
			var model = await _weatherResultViewModelBuilder.Build(location);
			if (model == null)
			{
				ModelState.AddModelError("invalid_location", "You are entered invalid location!");
				return View(nameof(Index));
			}
			return View(nameof(Index), model);
		}
	}
}
