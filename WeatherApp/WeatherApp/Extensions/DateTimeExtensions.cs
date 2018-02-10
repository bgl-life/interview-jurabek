using System;

namespace WeatherApp.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime UnixOffsetToDateTime(this long unixTimeSeconds) => 
			new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeSeconds).ToLocalTime();
	}
}
