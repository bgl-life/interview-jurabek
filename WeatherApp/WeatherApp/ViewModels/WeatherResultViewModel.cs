using System;

namespace WeatherApp.ViewModels
{
    public class WeatherResultViewModel
    {
	    public TemperatureViewModel Temperature { get; set; }

	    public string  Location { get; set; }

	    public DateTime Sunrise { get; set; }

	    public DateTime Sunset { get; set; }

	    public long Pressure { get; set; }

	    public int Humidity { get; set; }
    }
}
