using System;

namespace Core.Domain
{
	public class Instagram : IEntity<int>
	{
		public virtual int Id { get; set; }

		public virtual string InstagramId { get; set; }

		public virtual DateTime TimeCreated { get; set; }

		public virtual double TimeUnixCreated { get; set; }

		public virtual string Username { get; set; }

		public virtual string Url { get; set; }

		public virtual string ImageThumbnail { get; set; }

		public virtual string ImageLowResolution { get; set; }

		public virtual string ImageStandartResolution { get; set; }

		public virtual string Text { get; set; }
	}
}