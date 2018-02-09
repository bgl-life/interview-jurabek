using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Moq;
using WeatherApp.Abstraction.Services;
using WeatherApp.ModelBuilders;
using WeatherApp.Models;
using WeatherApp.ViewModels;
using Xunit;

namespace WeatherApp.UnitTests.ModelBuilders
{
    public class WeatherResultViewModelBuilderTests
    {
	    private readonly Mock<IMapper> _mapperMock;
	    private readonly Mock<IOpenWeatherMapService> _weatherMapServiceMock;
	    private readonly WeatherResultViewModelBuilder _weatherViewModelBuilder;

	    public WeatherResultViewModelBuilderTests()
	    {
		    _weatherMapServiceMock = new Mock<IOpenWeatherMapService>();
		    _mapperMock = new Mock<IMapper>();
		    _weatherViewModelBuilder = new WeatherResultViewModelBuilder(_weatherMapServiceMock.Object, _mapperMock.Object);
	    }

		[Fact]
	    public async Task Build_Returns_Null_When_Invalid_Location()
	    {
			// Act
		    _weatherMapServiceMock.Setup(x => x.GetWeatherByLocation("invalid")).Returns(Task.FromResult<OpenWeatherApiModel>(null));

		    // Arrange
		    var result = await _weatherViewModelBuilder.Build("invalid");

		    // Assert
			Assert.Null(result);
	    }

	    [Fact]
		public async Task Build_Returns_WeatherResultViewModel_When_Correct_Location()
	    {
		    // Act
			var fixture = new Fixture();
		    var apiModel = fixture.Create<OpenWeatherApiModel>();
		    var viewModel = fixture.Create<WeatherResultViewModel>();

		    _weatherMapServiceMock.Setup(x => x.GetWeatherByLocation("London")).Returns(Task.FromResult(apiModel));
		    _mapperMock.Setup(x => x.Map<WeatherResultViewModel>(apiModel)).Returns(viewModel);

		    // Arrange
		    var result = await _weatherViewModelBuilder.Build("London");

		    // Assert
			Assert.NotNull(result);
			Assert.Equal(viewModel, result);
	    }
	}
}
