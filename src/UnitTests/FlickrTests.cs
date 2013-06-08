using System;
using System.Collections.Generic;
using FlickrNet;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class FlickrTests
	{
		[Test]
		[Explicit]
		public void ApiSpike_GetInfo()
		{
			var client = new Flickr("dummyapikey", "dummysharedsecret");
			client.InstanceCacheDisabled = true;

			var photos = client.PhotosSearch(new PhotoSearchOptions("dummyuserid", "blog"));

			var result = new List<Tuple<string,string>>();

			foreach (var photo in photos)
			{
				var info = client.PhotosGetInfo(photo.PhotoId, photo.Secret);
				result.Add(new Tuple<string, string>(info.Title, info.Description));
			}

			Assert.That(result, Is.Not.Empty);
		}
	}
}