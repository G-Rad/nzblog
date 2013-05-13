using Core.Domain;
using Core.Domain.Enums;

namespace Core.Repositories
{
	public interface ISettingsRepository : IBaseRepository<Setting, int>
	{
		T GetSetting<T>(SettingType settingType);

		void SaveSetting(SettingType settingType, string value);
	}
}