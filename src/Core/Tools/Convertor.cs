using System;

namespace Core.Tools
{
	public class Convertor
	{
		public static T Convert<T>(string value)
		{
			if (typeof (T) == typeof (DateTime))
				return (T)(object)DateTime.Parse(value);

			if (typeof(T) == typeof(DateTime?))
				return (T)(object)DateTime.Parse(value);

			if (typeof(T) == typeof(int))
				return (T)(object)int.Parse(value);

			if (typeof (T) == typeof (string))
				return (T)(object)value;

			throw new NotImplementedException();
		}
	}
}