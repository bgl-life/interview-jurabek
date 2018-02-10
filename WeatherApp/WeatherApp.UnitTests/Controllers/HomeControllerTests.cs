using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherApp.Abstraction.ModelBuilders;
using WeatherApp.Constants;
using WeatherApp.Controllers;
using WeatherApp.ViewModels;
using Xunit;

namespace WeatherApp.UnitTests.Controllers
{
    public class HomeControllerTests
    {
		private readonly Mock<IWeatherResultViewModelBuilder> _weatherResultViewModelBuilder;
		private readonly HomeController _homeController;

		public HomeControllerTests()
		{
			_weatherResultViewModelBuilder = new Mock<IWeatherResultViewModelBuilder>();
			_homeController = new HomeController(_weatherResultViewModelBuilder.Object);
	    }

		[Fact]
	    public void Index_Returns_View()
		{
			// Arrange
			var result = _homeController.Index();

			// Assert
			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
		}

		[Fact]
	    public void Search_ShouldAddModelErrorsAndViewShouldBeIndex_WhenLocationIsInvalid()
	    {
			// Act
		    _weatherResultViewModelBuilder.Setup(x => x.Build("invalid")).Returns(Task.FromResult<WeatherResultViewModel>(null));
			
		    // Arrange
			var result = _homeController.Search("invalid").Result;

		    // Assert
		    Assert.NotNull(result);
		    Assert.Equal("Index", ((ViewResult)result).ViewName);
			Assert.False(_homeController.ModelState.IsValid);
			Assert.Equal("You are entered invalid location!", _homeController.ModelState[AppConstants.InvalidLocationKey].Errors[0].ErrorMessage);
		}

		[Fact]
	    public async Task Search_ReturnsResultViewModelAndIndexView_WhenValidLocation()
	    {
		    // Act
		    var resultViewModel = new Fixture().Create<WeatherResultViewModel>();
		    _weatherResultViewModelBuilder.Setup(x => x.Build("London")).Returns(Task.FromResult(resultViewModel));

		    // Arrange
		    var result = await _homeController.Search("London") as ViewResult;

			// Assert
		    Assert.True(_homeController.ModelState.IsValid);
			Assert.NotNull(result);
			Assert.IsType<WeatherResultViewModel>(result.Model);
			Assert.Equal(resultViewModel, result.Model);
			Assert.Equal("Index", result.ViewName);
		}
	}
}
