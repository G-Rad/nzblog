namespace Core.Domain
{
	public class Flick : IEntity<int>
	{
		private const string Format1 = "http://farm{0}.staticflickr.com/{1}/{2}_{3}_{4}.jpg";

		public virtual int Id { get; set; }

		public virtual string FlickrId { get; set; }

		public virtual string Title { get; set; }
		
		public virtual string Description { get; set; }

		public virtual string ServerId { get; set; }

		public virtual string FarmId { get; set; }

		public virtual string Secret { get; set; }

		public virtual string UrlImageMedium
		{
			get { return string.Format(Format1, FarmId, ServerId, FlickrId, Secret, "c"); }
		}

		public virtual string UrlImageSquareLarge
		{
			get { return string.Format(Format1, FarmId, ServerId, FlickrId, Secret, "q"); }
		}
	}
}