using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Core.Domain;
using Core.Domain.Enums;
using Core.Repositories;
using NLog;
using Newtonsoft.Json.Linq;

namespace Core.Services
{
	public interface IInstagramService
	{
		bool ShouldUpdate();
		void StartUpdate(ILifetimeScope lifetimeScope);
	}

	public class InstagramService : BaseService, IInstagramService
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		private static readonly object _locker = new object();

		private readonly ISettingsRepository _settingsRepository;
		private readonly IApplicationSettingsProvider _applicationSettingsProvider;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IInstagramRepository _instagramRepository;
		private ILifetimeScope _lifetimeScope;

		public InstagramService(ISettingsRepository settingsRepository,
			IApplicationSettingsProvider applicationSettingsProvider,
			IUnitOfWork unitOfWork,
			IInstagramRepository instagramRepository) : base(applicationSettingsProvider, settingsRepository)
		{
			_settingsRepository = settingsRepository;
			_applicationSettingsProvider = applicationSettingsProvider;
			_unitOfWork = unitOfWork;
			_instagramRepository = instagramRepository;
		}

		public bool ShouldUpdate()
		{
			return base.ShouldUpdateInternal(ApplicationSettingType.InstagramLastUpdate, SettingType.InstagramLastUpdate);
		}

		public void StartUpdate(ILifetimeScope lifetimeScope)
		{
			_lifetimeScope = lifetimeScope;

			new Task(FatchAndUpdate).Start();
		}

		private void FatchAndUpdate()
		{
			Logger.Trace("FatchAndUpdate is started");

			var lockWasTaken = false;

			try
			{
				Monitor.Enter(_locker, ref lockWasTaken);

				if (!ShouldUpdate())
				{
					Logger.Trace("Should update returned false. Exit from FatchAndUpdate");
					return;
				}

				_applicationSettingsProvider.SaveSetting(ApplicationSettingType.InstagramIsUpdating, true.ToString());

				var instagramList = GetRecentPosts();

				SaveFetchedData(instagramList);

				_applicationSettingsProvider.SaveSetting(ApplicationSettingType.InstagramLastUpdate, DateTime.Now.ToString());
			}
			catch (Exception ex)
			{
				Logger.ErrorException("Exception during Instagram Updating", ex);
			}
			finally
			{
				if (lockWasTaken) Monitor.Exit(_locker);

				_applicationSettingsProvider.SaveSetting(ApplicationSettingType.InstagramIsUpdating, false.ToString());
			}

			_lifetimeScope.Dispose();
		}

		private void SaveFetchedData(IEnumerable<Instagram> instagramList)
		{
			Logger.Trace("SaveFetchedData is started");

			using (var unit = _unitOfWork.BeginTransaction())
			{
				var existedRecords = _instagramRepository.GetAll();
				Logger.Trace("All records have been fetched from DB");

				foreach (var newRecord in instagramList.OrderBy(x => x.TimeCreated))
				{
					if (existedRecords.All(x => x.InstagramId != newRecord.InstagramId))
					{
						Logger.Trace("Instagram service: saving of a new instagram item: " + newRecord.InstagramId);
						_instagramRepository.Save(newRecord);
					}
				}

				var updateSetting = _settingsRepository.GetAll().First(x => x.SettingsKey == "instagram.lastupdate");
				updateSetting.Value = DateTime.Now.ToString();
				_settingsRepository.Save(updateSetting);
				Logger.Trace("LastUpdate setting has been updated in DB");

				unit.Commit();
			}

			Logger.Trace("SaveFetchedData is finished");
		}

		private IEnumerable<Instagram> GetRecentPosts()
		{
			var instagramList = new List<Instagram>();

			var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

			var urlToFetch = string.Format(
				"https://api.instagram.com/v1/users/{0}/media/recent/?access_token={1}", 
				Settings.Instagram.UserId,
				Settings.Instagram.AccessToken);

			Logger.Trace("Instagram service: fetch " + urlToFetch);
			
			using (var cliend = new WebClient())
			{
				var jsonString = cliend.DownloadString(urlToFetch);
				dynamic d = JValue.Parse(jsonString);

				foreach (var dataItem in d.data)
				{
					var instagram = new Instagram
					{
						InstagramId = dataItem.id,
						Url = dataItem.link,
						Username = dataItem.user.username,
						Text = dataItem.caption.text,
						TimeCreated = dtDateTime.AddSeconds(double.Parse((string)dataItem.created_time)),
						TimeUnixCreated = double.Parse((string)dataItem.created_time),
						ImageLowResolution = dataItem.images.low_resolution.url,
						ImageStandartResolution = dataItem.images.standard_resolution.url,
						ImageThumbnail = dataItem.images.thumbnail.url
					};
					instagramList.Add(instagram);
				}
			}

			return instagramList;
		}
	}
}