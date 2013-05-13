using System;
using System.Linq;
using Core.Domain;
using Core.Domain.Enums;
using Core.Tools;
using NLog;

namespace Core.Repositories
{
	public class SettingsRepository : BaseRepository<Setting, int>, ISettingsRepository
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public SettingsRepository(INHibernateUnitOfWork unitOfWorkFactory) : base(unitOfWorkFactory)
		{
			Logger.Trace("SettingsRepository created");
		}

		public T GetSetting<T>(SettingType settingType)
		{
			var key = "";

			switch (settingType)
			{
				case SettingType.InstagramLastUpdate:
					key = "instagram.lastupdate";
					break;

				case SettingType.FlickrLastUpdate:
					key = "flickr.lastupdate";
					break;

				default:
					throw new NotImplementedException(settingType.ToString());
			}

			var item = GetAll().First(x => x.SettingsKey == key);

			var result = Convertor.Convert<T>(item.Value);

			return result;
		}

		public void SaveSetting(SettingType settingType, string value)
		{
			var item = GetAll().First(x => x.SettingsKey == "instagram.lastupdate");

			item.Value = value;

			Update(item);
		}
	}
}