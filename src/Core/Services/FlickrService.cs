using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Core.Domain;
using Core.Domain.Enums;
using Core.Repositories;
using FlickrNet;
using NLog;

namespace Core.Services
{
	public interface IFlickrService
	{
		bool ShouldUpdate();
		void StartUpdate(ILifetimeScope lifetimeScope);
	}

	public class FlickrService : BaseService, IFlickrService
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private static readonly object _locker = new object();

		private readonly IApplicationSettingsProvider _applicationSettingsProvider;
		private readonly ISettingsRepository _settingsRepository;
		private readonly IFlickrRepository _flickrRepository;
		private readonly IUnitOfWork _unitOfWork;

		private ILifetimeScope _lifetimeScope;

		public FlickrService(
			IApplicationSettingsProvider applicationSettingsProvider, 
			ISettingsRepository settingsRepository,
			IFlickrRepository flickrRepository,
			IUnitOfWork unitOfWork
			) : base(
			applicationSettingsProvider, 
			settingsRepository
			)
		{
			_applicationSettingsProvider = applicationSettingsProvider;
			_settingsRepository = settingsRepository;
			_flickrRepository = flickrRepository;
			_unitOfWork = unitOfWork;
		}

		public bool ShouldUpdate()
		{
			return base.ShouldUpdateInternal(ApplicationSettingType.FlickrLastUpdate, SettingType.FlickrLastUpdate);
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

				_applicationSettingsProvider.SaveSetting(ApplicationSettingType.FlickrIsUpdating, true.ToString());

				var photosList = GetRecentPosts();

				SaveFetchedData(photosList);

				_applicationSettingsProvider.SaveSetting(ApplicationSettingType.FlickrLastUpdate, DateTime.Now.ToString());
			}
			catch (Exception ex)
			{
				Logger.ErrorException("Exception during Instagram Updating", ex);
			}
			finally
			{
				if (lockWasTaken) Monitor.Exit(_locker);

				_applicationSettingsProvider.SaveSetting(ApplicationSettingType.FlickrIsUpdating, false.ToString());
			}

			_lifetimeScope.Dispose();
		}

		private void SaveFetchedData(IEnumerable<Flick> instagramList)
		{
			Logger.Trace("SaveFetchedData is started");

			using (var unit = _unitOfWork.BeginTransaction())
			{
				var existedRecords = _flickrRepository.GetAll();
				Logger.Trace("All records have been fetched from DB");

				foreach (var newRecord in instagramList.Reverse())
				{
					if (existedRecords.All(x => x.FlickrId != newRecord.FlickrId))
						_flickrRepository.Save(newRecord);
				}

				var updateSetting = _settingsRepository.GetAll().First(x => x.SettingsKey == "flickr.lastupdate");
				updateSetting.Value = DateTime.Now.ToString();
				_settingsRepository.Save(updateSetting);
				Logger.Trace("LastUpdate setting has been updated in DB");

				unit.Commit();
			}

			Logger.Trace("SaveFetchedData is finished");
		}

		private IEnumerable<Flick> GetRecentPosts()
		{
			var client = new Flickr("dummyapikey", "dummysharedsecret");
			client.InstanceCacheDisabled = true;

			var photos = client.PhotosSearch(new PhotoSearchOptions("dummyuserid", "blog"));

			var result = new List<Flick>();

			foreach (var photo in photos)
			{
				var flick = new Flick
								{
									FlickrId = photo.PhotoId,
									Title = photo.Title,
									Secret = photo.Secret,
									FarmId = photo.Farm,
									ServerId = photo.Server
								};
				result.Add(flick);
			}

			return result;
		}
	}
}