using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services;

namespace Core.Domain
{
	public class Flick : IEntity<int>
	{
		private readonly IFlickrService _flickrService;

		private string _tags = "";

		public Flick(IFlickrService flickrService)
		{
			_flickrService = flickrService;
		}

		private const string Format1 = "http://farm{0}.staticflickr.com/{1}/{2}_{3}_{4}.jpg";
		private const string Format2 = "http://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg";

		public virtual int Id { get; set; }

		public virtual string FlickrId { get; set; }

		public virtual string Title { get; set; }
		
		public virtual string Description { get; set; }

		public virtual string ServerId { get; set; }

		public virtual string FarmId { get; set; }

		public virtual string Secret { get; set; }

		public virtual IEnumerable<string> Tags
		{
			get
			{
				var tags = new string[0];

				if (_tags != null)
				{
					tags = _tags
						.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
						.Select(x => x.Trim().ToLower())
						.ToArray();
				}

				return tags;
			}
		}

		public virtual void AddTag(string tag)
		{
			if (!Tags.Contains(tag))
			{
				_tags += "," + tag.Trim().ToLower();
			}
		}

		public virtual string UrlImageSquareSmall
		{
			get { return string.Format(Format1, FarmId, ServerId, FlickrId, Secret, "s"); }
		}

		public virtual string UrlImageMedium500
		{
			get { return string.Format(Format2, FarmId, ServerId, FlickrId, Secret); }
		}

		public virtual string UrlImageMedium800
		{
			get { return string.Format(Format1, FarmId, ServerId, FlickrId, Secret, "c"); }
		}

		public virtual string UrlImageSquareLarge
		{
			get { return string.Format(Format1, FarmId, ServerId, FlickrId, Secret, "q"); }
		}

		public virtual void Update()
		{
			_flickrService.UpdatePhoto(this);
		}
	}
}