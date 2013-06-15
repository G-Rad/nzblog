using System;
using System.Text.RegularExpressions;
using Core.Repositories;

namespace Blog.Core.Templating
{
	public class PhotoTemplate
	{
		public static readonly PhotoTemplate Instance = new PhotoTemplate();

		public string ReplaceCode(string bodyToReplace)
		{
			var replacedBody = bodyToReplace;

			var matches = Regex.Matches(replacedBody, @"#{flickrphoto\(([\d,]*)\)}");
			for (int i = 0; i < matches.Count; i++)
			{
				replacedBody = replacedBody.Replace(
					matches[i].Value,
					"@PhotoTemplate.Instance.Render(Model.FlickrRepository, \"" + matches[i].Groups[1] + "\")");
			}

			return replacedBody;
		}

		public string Render(IFlickrRepository flickrRepository, string id)
		{
			var result = "<div class='post-image'>";

			var photo = flickrRepository.GetById(int.Parse(id));
			result +=
					string.Format("<a class='fancybox' href='{0}' title='{1}'><img border='0' alt='{2}' src='{2}'></a>", 
					photo.UrlImageMedium800, photo.Description, photo.UrlImageMedium500);

			result += "</div>";

			return result;
		}
	}
}