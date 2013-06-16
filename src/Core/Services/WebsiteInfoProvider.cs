using System.Collections.Generic;
using Core.Repositories;

namespace Core.Services
{
	public interface IWebsiteInfoProvider
	{
		IList<string> GetInfo();
	}

	public class WebsiteInfoProvider : IWebsiteInfoProvider
	{
		private readonly IFlickrRepository _flickrRepository;
		private readonly IInstagramRepository _instagramRepository;

		private readonly IList<string> _infoList = new List<string>();

		public WebsiteInfoProvider(
			IFlickrRepository flickrRepository, 
			IInstagramRepository instagramRepository)
		{
			_flickrRepository = flickrRepository;
			_instagramRepository = instagramRepository;
		}

		public IList<string> GetInfo()
		{
			GetFlickrInfo();
			GetInstagramInfo();

			return _infoList;
		}

		private void GetInstagramInfo()
		{
			var imagesAmount = _instagramRepository.GetAll().Length;
			_infoList.Add(string.Format("Instagram photos: {0}", imagesAmount));
		}

		private void GetFlickrInfo()
		{
			var imagesAmount = _flickrRepository.GetAll().Length;
			_infoList.Add(string.Format("Flickr photos: {0}", imagesAmount));
		}
	}
}