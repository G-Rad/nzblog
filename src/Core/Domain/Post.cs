using System;
using Core.Extensions;

namespace Core.Domain
{
	public class Post : IEntity<int>
	{
		public virtual int Id { get; set; }

		public virtual string Title { get; set; }

		public virtual string Body { get; set; }

		public virtual string Thumbnail { get; set; }

		public virtual string Url { get; set; }

		public virtual DateTime DateCreated { get; set; }

		public virtual DateTime DatePublished { get; set; }

		public virtual bool IsPublished { get; set; }

		public virtual string MetaDescription { get; set; }
		
		public virtual string MetaImageUrl { get; set; }

		public virtual string Summary
		{
			get
			{
				if (Body.Contains("<a name='cut'></a>"))
					return Body.Split(new[] {"<a name='cut'></a>"}, StringSplitOptions.RemoveEmptyEntries)[0];

				const int summaryLength = 500;
				if (Body.Length < summaryLength)
					return Body;

				return Body.TruncateAtWord(summaryLength) + "...";
			}
		}
	}
}