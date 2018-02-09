using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Abstraction.Services
{
    public interface IOpenWeatherMapService
    {
	    Task<OpenWeatherApiModel> GetWeatherByLocation(string location);
    }
}
