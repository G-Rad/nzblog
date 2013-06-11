using System.Configuration;

namespace Core.Services
{
	public class Settings
	{
		public static class Flickr
		{
			public static string ApiKey
			{
				get { return ConfigurationManager.AppSettings["flirk.apikey"]; }
			}

			public static string SharedSecret
			{
				get { return ConfigurationManager.AppSettings["flirk.sharedsecret"]; }
			}

			public static string UserId
			{
				get { return ConfigurationManager.AppSettings["flirk.userid"]; }
			}
		}

		public static class Instagram
		{
			public static string UserId
			{
				get { return ConfigurationManager.AppSettings["instagram.userid"]; }
			}

			public static string AccessToken
			{
				get { return ConfigurationManager.AppSettings["instagram.accesstoken"]; }
			}
		}
	}
}