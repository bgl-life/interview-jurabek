using System.Linq;
using AutoMapper;
using WeatherApp.Extensions;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Mappings
{
    public class MappingProfile : Profile
    {
	    public MappingProfile()
	    {
		    CreateMap<OpenWeatherApiModel, WeatherResultViewModel>()
			    .ForMember(x => x.Humidity, map => map.MapFrom(x => x.Main.Humidity))
			    .ForMember(x => x.Location, map => map.MapFrom(x => x.Name))
			    .ForMember(x => x.Pressure, map => map.MapFrom(x => x.Main.Pressure))
			    .ForMember(x => x.Sunrise, map => map.MapFrom(x => x.Sys.Sunrise.UnixOffsetToDateTime()))
			    .ForMember(x => x.Sunset, map => map.MapFrom(x => x.Sys.Sunset.UnixOffsetToDateTime()))
			    .ForMember(x => x.Temperature, 
				    map => map.MapFrom(x => new TemperatureViewModel
					{
						Description = x.Weather.FirstOrDefault().Main,
						Current = x.Main.Temp,
						Max = x.Main.TempMax,
						Min = x.Main.TempMin,
						Icon = $"http://openweathermap.org/img/w/{x.Weather.FirstOrDefault().Icon}.png"
					}));

	    }
    }
}
