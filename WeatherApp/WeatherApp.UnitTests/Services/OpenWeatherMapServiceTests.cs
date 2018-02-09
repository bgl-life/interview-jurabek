using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using WeatherApp.Abstraction.Facades;
using WeatherApp.Constants;
using WeatherApp.Services;
using Xunit;

namespace WeatherApp.UnitTests.Services
{	
    public class OpenWeatherMapServiceTests
    {
	    private readonly Mock<IConfiguration> _configurationMock;
	    private readonly Mock<IHttpClientFacade> _httpClientFacadeMock;
	    private readonly OpenWeatherMapService _openWeatherMapService;

	    public OpenWeatherMapServiceTests()
	    {
		    _configurationMock = new Mock<IConfiguration>();
			_httpClientFacadeMock = new Mock<IHttpClientFacade>();
		    _openWeatherMapService = new OpenWeatherMapService(_configurationMock.Object, _httpClientFacadeMock.Object);
	    }

		[Fact]
	    public async Task GetWeatherByLocation_Returns_null_When_Invalid_Location()
		{
			// Act
			var location = "invalid_location";
			var baseUri = "http://api.weather.com";
			var apiKey = "abc_key";
			var requestUri = $"http://api.weather.com?q={location}&appid{apiKey}&units=metric";
			var response = new HttpResponseMessage(HttpStatusCode.NotFound);

			_configurationMock.SetupGet(x => x[AppConstants.OpenWeatherMapBaseUri]).Returns(baseUri);
			_configurationMock.SetupGet(x => x[AppConstants.OpenWeatherMapApiKey]).Returns(apiKey);
			_httpClientFacadeMock.Setup(x => x.GetRequest(baseUri, It.IsAny<IDictionary<string, string>>())).Returns(requestUri);
			_httpClientFacadeMock.Setup(x => x.GetAsync(requestUri)).Returns(Task.FromResult(response));

			// Arrange
			var result = await _openWeatherMapService.GetWeatherByLocation(location);

			// Assert
			Assert.Null(result);
		}

		[Fact]
	    public async Task GetWeatherByLocation_Returns_OpenWeatherApiModel_When_Valid_Location()
	    {
		    // Act
		    var location = "London";
		    var baseUri = "http://api.weather.com";
		    var apiKey = "abc_key";
		    var requestUri = $"http://api.weather.com?q={location}&appid{apiKey}&units=metric";
		    var stringContent = @"{""coord"":{""lon"":-0.13,""lat"":51.51},""weather"":[{""id"":521,""main"":""Rain"",""description"":""shower rain"",""icon"":""09d""}],""base"":""stations"",""main"":{""temp"":4.35,""pressure"":1009,""humidity"":95,""temp_min"":3,""temp_max"":6},""visibility"":10000,""wind"":{""speed"":7.7,""deg"":290},""clouds"":{""all"":90},""dt"":1518178800,""sys"":{""type"":1,""id"":5091,""message"":0.0038,""country"":""GB"",""sunrise"":1518161085,""sunset"":1518195927},""id"":2643743,""name"":""London"",""cod"":200}";
		    var response = new HttpResponseMessage { Content = new StringContent(stringContent) };

		    _configurationMock.SetupGet(x => x[AppConstants.OpenWeatherMapBaseUri]).Returns(baseUri);
		    _configurationMock.SetupGet(x => x[AppConstants.OpenWeatherMapApiKey]).Returns(apiKey);
		    _httpClientFacadeMock.Setup(x => x.GetRequest(baseUri, It.IsAny<IDictionary<string, string>>())).Returns(requestUri);
		    _httpClientFacadeMock.Setup(x => x.GetAsync(requestUri)).Returns(Task.FromResult(response));

		    // Arrange
		    var result = await _openWeatherMapService.GetWeatherByLocation(location);

		    // Assert
			Assert.NotNull(result);
			Assert.Equal(location, result.Name);
			Assert.Equal(4.35, result.Main.Temp);
			Assert.Equal(3, result.Main.TempMin);
			Assert.Equal(6, result.Main.TempMax);
			Assert.Equal(1009, result.Main.Pressure);
			Assert.Equal(95, result.Main.Humidity);
			Assert.Equal(1518195927, result.Sys.Sunset);
			Assert.Equal(1518161085, result.Sys.Sunrise);
		}
	}
}
