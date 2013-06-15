using System;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Repositories;

namespace Blog.Core.Templating
{
	public class PhotosTemplate
	{
		public static readonly PhotosTemplate Instance = new PhotosTemplate();

		public string ReplaceCode(string bodyToReplace)
		{
			var replacedBody = bodyToReplace;

			var matches = Regex.Matches(replacedBody, @"#{flickrphotos\(([\d,]*)\)}");
			for (int i = 0; i < matches.Count; i++)
			{
				replacedBody = replacedBody.Replace(
					matches[i].Value,
					"@PhotosTemplate.Instance.Render(Model.FlickrRepository, \"" + matches[i].Groups[1] + "\")");
			}

			return replacedBody;
		}

		public string Render(IFlickrRepository flickrRepository, string ids)
		{
			var splittedId = ids.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
			var idList = splittedId.Select(int.Parse).ToList();

			var result = "<div class='post-imageset-gallery'>";
			foreach (var id in idList)
			{
				var photo = flickrRepository.GetById(id);
				result +=
					string.Format("<a class='fancybox' href='{0}' rel='group' title='{1}'><img alt='' src='{2}'></a>", 
					photo.UrlImageMedium800, photo.Description, photo.UrlImageSquareLarge);
			}
			result += "</div>";

			return result;
		}
	}
}