using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Domain;
using Core.Repositories;

namespace Blog.Core.Templating
{
	public class PhotosTagTemplate
	{
		public static readonly PhotosTagTemplate Instance = new PhotosTagTemplate();

		public string ReplaceCode(string bodyToReplace)
		{
			var replacedBody = bodyToReplace;

			var matches = Regex.Matches(replacedBody, @"#{flickrphotos\(([A-Za-z0-9,]*)\)}");
			for (int i = 0; i < matches.Count; i++)
			{
				replacedBody = replacedBody.Replace(
					matches[i].Value,
					"@PhotosTagTemplate.Instance.Render(Model.FlickrRepository, \"" + matches[i].Groups[1] + "\")");
			}

			return replacedBody;
		}

		public string Render(IFlickrRepository flickrRepository, string tags)
		{
			var splittedTags = tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => x.ToLower().Trim());

			var photos = new List<Flick>();

			foreach (var tag in splittedTags)
			{
				photos.AddRange(flickrRepository.GetByTag(tag));
			}

			photos = SelectUniquePhotos(photos);

			var result = "<div class='post-imageset-gallery'>";
			foreach (var photo in photos)
			{
				result +=
					string.Format("<a class='fancybox' href='{0}' rel='group' title='{1}'><img alt='' src='{2}'></a>", 
					photo.UrlImageMedium800, photo.Description, photo.UrlImageSquareLarge);
			}
			result += "</div>";

			return result;
		}

		private static List<Flick> SelectUniquePhotos(List<Flick> photos)
		{
			return photos.GroupBy(cust => cust.Id).Select(grp => grp.First()).ToList();
		}
	}
}