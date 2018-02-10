using Newtonsoft.Json;

namespace WeatherApp.Models
{
	public class Clouds
	{
		[JsonProperty("all")]
		public long All { get; set; }
	}
}