using System;

namespace WeatherApp.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime UnixOffsetToDateTime(this long unixTimeSeconds)
		{
			var offset =  DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds);
			return new DateTime(offset.Ticks);
		}
	}
}
