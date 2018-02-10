﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherApp.Models
{
	public class OpenWeatherApiModel
	{
		[JsonProperty("coord")]
		public Coord Coord { get; set; }

		[JsonProperty("weather")]
		public List<Weather> Weather { get; set; }

		[JsonProperty("base")]
		public string Base { get; set; }

		[JsonProperty("main")]
		public Main Main { get; set; }

		[JsonProperty("visibility")]
		public long Visibility { get; set; }

		[JsonProperty("wind")]
		public Wind Wind { get; set; }

		[JsonProperty("clouds")]
		public Clouds Clouds { get; set; }

		[JsonProperty("dt")]
		public long Dt { get; set; }

		[JsonProperty("sys")]
		public Sys Sys { get; set; }

		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("cod")]
		public long Cod { get; set; }
	}
}
