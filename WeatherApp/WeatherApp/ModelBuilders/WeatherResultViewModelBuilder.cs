using System.Threading.Tasks;
using AutoMapper;
using WeatherApp.Abstraction.ModelBuilders;
using WeatherApp.Abstraction.Services;
using WeatherApp.ViewModels;

namespace WeatherApp.ModelBuilders
{
    public class WeatherResultViewModelBuilder : IWeatherResultViewModelBuilder
	{
		private readonly IOpenWeatherMapService _openWeatherMapService;
		private readonly IMapper _mapper;

		public WeatherResultViewModelBuilder(
			IOpenWeatherMapService openWeatherMapService,
			IMapper mapper)
		{
			_openWeatherMapService = openWeatherMapService;
			_mapper = mapper;
		}

	    public async Task<WeatherResultViewModel> Build(string location)
	    {
		    if (string.IsNullOrEmpty(location))
			    return null;

		    var weatherResult = await _openWeatherMapService.GetWeatherByLocation(location);
		    if (weatherResult == null)
			    return null;

			return _mapper.Map<WeatherResultViewModel>(weatherResult);
		}
    }
}
