using System;
using Core.Domain.Enums;
using Core.Repositories;
using NLog;

namespace Core.Services
{
	public class BaseService
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		private readonly IApplicationSettingsProvider _applicationSettingsProvider;
		private readonly ISettingsRepository _settingsRepository;

		public BaseService(
			IApplicationSettingsProvider applicationSettingsProvider,
			ISettingsRepository settingsRepository)
		{
			_applicationSettingsProvider = applicationSettingsProvider;
			_settingsRepository = settingsRepository;
		}

		protected virtual bool ShouldUpdateInternal(ApplicationSettingType applicationSettingType, SettingType settingType)
		{
			Logger.Trace("ShouldUpdate started. Application setting: {0}", applicationSettingType);

			var lastUpdate = _applicationSettingsProvider.GetSettingOrDefault<DateTime?>(applicationSettingType);

			if (!lastUpdate.HasValue)
			{
				Logger.Trace("Application settins is empty. Start fetching settings from DB");

				lastUpdate = _settingsRepository.GetSetting<DateTime>(settingType);

				Logger.Trace("Application settins have been fetched from DB. Last update was {0}", lastUpdate);

				_applicationSettingsProvider.SaveSetting(applicationSettingType, lastUpdate.ToString());

				Logger.Trace("Last updating time has been saved to application state");
			}

			var result = ((DateTime.Now - (DateTime)lastUpdate).TotalSeconds > 21600);

			Logger.Trace("Result of ShouldUpdate is {0}", result);

			return result;
		} 
	}
}