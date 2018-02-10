using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Xunit;

namespace WeatherApp.IntegrationTests
{
	public class OneTimeSetupTestServerFixture : IDisposable
	{
		private readonly TestServer _server;
		public OneTimeSetupTestServerFixture()
		{
			// Arrange
			var path = PlatformServices.Default.Application.ApplicationBasePath;
			var setDir = Path.GetFullPath(Path.Combine(path, "../../../../WeatherApp"));

			var configuration = new ConfigurationBuilder()
				.SetBasePath(setDir)
				.AddJsonFile("appsettings.json").Build();

			var builder = new WebHostBuilder()
				.UseEnvironment("Development")
				.UseContentRoot(setDir)
				.UseConfiguration(configuration)
				.UseStartup<Startup>();

			_server = new TestServer(builder);
		}

		public HttpClient GetClient() => _server.CreateClient();

		public void Dispose() => _server.Dispose();
	}

	public class HomeControllerIntegrationTests : IClassFixture<OneTimeSetupTestServerFixture>
	{
		private readonly HttpClient _client;
		public HomeControllerIntegrationTests(OneTimeSetupTestServerFixture fixture)
		{
			_client = fixture.GetClient();
		}

		[Fact]
		public async Task Index_ReturnsResulView()
		{
			// Act
			var response = await _client.GetAsync("/Home/Index");
			string content = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.NotNull(content);
		}
		[Fact]
		public async Task Search_ReturnsResultViewWithWeatherData_WhenValidLocation()
		{
			// Act
			var response = await _client.GetAsync("/Home/Search?q=London");
			string content = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.NotNull(content);
			Assert.Contains("London", content);
		}

		[Fact]
		public async Task Search_ReturnsErrorMessage_WhenInvalidLocation()
		{
			// Act
			var response = await _client.GetAsync("/Home/Search?q=invalid_asafafas");
			string content = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.NotNull(content);
			Assert.Contains("You are entered invalid location!", content);
		}
	}
}
