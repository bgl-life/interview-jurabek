using System.Threading.Tasks;
using WeatherApp.ViewModels;

namespace WeatherApp.Abstraction.ModelBuilders
{
	public interface IWeatherResultViewModelBuilder
	{
		Task<WeatherResultViewModel> Build(string location);
	}
}
