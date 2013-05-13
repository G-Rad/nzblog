using System;
using System.Web;
using Core.Domain.Enums;
using Core.Tools;

namespace Core.Services
{
	public interface IApplicationSettingsProvider
	{
		bool HasSetting(ApplicationSettingType applicationSettingType);
		T GetSetting<T>(ApplicationSettingType applicationSettingType);
		T GetSettingOrDefault<T>(ApplicationSettingType applicationSettingType);
		void SaveSetting(ApplicationSettingType applicationSettingType, string value);
	}

	public class ApplicationSettingsProvider : IApplicationSettingsProvider
	{
		private HttpContext _context;

		private HttpContext Context
		{
			get
			{
				if (_context == null)
					return HttpContext.Current;

				return _context;
			}
		}

		public ApplicationSettingsProvider()
		{
			_context = HttpContext.Current;
		}

		public ApplicationSettingsProvider(HttpContext context)
		{
			_context = context;
		}

		public void InjectContext(HttpContext context)
		{
			_context = context;
		}

		public bool HasSetting(ApplicationSettingType applicationSettingType)
		{
			return Context.Application[applicationSettingType.ToString()] != null;
		}

		public T GetSetting<T>(ApplicationSettingType applicationSettingType)
		{
			if (!HasSetting(applicationSettingType))
				throw new Exception("");

			var value = Context.Application[applicationSettingType.ToString()];

			var result = Convertor.Convert<T>(value.ToString());

			return result;
		}

		public T GetSettingOrDefault<T>(ApplicationSettingType applicationSettingType)
		{
			if (!HasSetting(applicationSettingType))
				return default(T);

			return GetSetting<T>(applicationSettingType);
		}

		public void SaveSetting(ApplicationSettingType applicationSettingType, string value)
		{
			Context.Application[applicationSettingType.ToString()] = value;
		}
	}
}