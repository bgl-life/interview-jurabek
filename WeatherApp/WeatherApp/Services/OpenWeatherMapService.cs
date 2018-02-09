using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WeatherApp.Abstraction.Facades;
using WeatherApp.Abstraction.Services;
using WeatherApp.Constants;
using WeatherApp.Models;

namespace WeatherApp.Services
{
	public class OpenWeatherMapService : IOpenWeatherMapService
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpClientFacade _httpClientFacade;

		public OpenWeatherMapService(
			IConfiguration configuration, 
			IHttpClientFacade httpClientFacade)
		{
			_configuration = configuration;
			_httpClientFacade = httpClientFacade;
		}

		public async Task<OpenWeatherApiModel> GetWeatherByLocation(string location)
		{
			var baseUri = _configuration[AppConstants.OpenWeatherMapBaseUri];
			var apiKey = _configuration[AppConstants.OpenWeatherMapApiKey];

			var queryParameters = new Dictionary<string, string>
			{
				{ "q", location },
				{ "appid", apiKey },
				{ "units" , "metric" }
			};

			var requestUri = _httpClientFacade.GetRequest(baseUri, queryParameters);

			var response = await _httpClientFacade.GetAsync(requestUri);
			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			var content = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<OpenWeatherApiModel>(content);
		}
	}
}
