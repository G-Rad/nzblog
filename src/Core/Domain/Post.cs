using System;
using Blog.Core.Templating;
using Core.Extensions;
using Core.Repositories;
using RazorTemplates.Core;

namespace Core.Domain
{
	public class Post : IEntity<int>
	{
		private readonly IFlickrRepository _flickrRepository;

		public Post(IFlickrRepository flickrRepository)
		{
			_flickrRepository = flickrRepository;
		}

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
				//TODO: use regex instead of duplication

				if (Body.Contains("<a name='cut'></a>"))
					return Body.Split(new[] {"<a name='cut'></a>"}, StringSplitOptions.RemoveEmptyEntries)[0];
				
				if (Body.Contains("<a name=\"cut\"></a>"))
					return Body.Split(new[] { "<a name=\"cut\"></a>" }, StringSplitOptions.RemoveEmptyEntries)[0];

				const int summaryLength = 500;
				if (Body.Length < summaryLength)
					return Body;

				return Body.TruncateAtWord(summaryLength) + "...";
			}
		}

		public virtual string FormattedBody
		{
			get { return FormatBody(); }
		}

		private string FormatBody()
		{
			var readyForRender = ReplaceCodesToRazorSyntax();

			var template = Template
				.WithBaseType<TemplateBase>()
				.AddNamespace("Blog.Core.Templating")
				.Compile(readyForRender);

			var model = new {FlickrRepository = _flickrRepository};

			var formattedBody = template.Render(model);
			return formattedBody;
		}

		private string ReplaceCodesToRazorSyntax()
		{
			var result = Body;
			result = PhotoTemplate.Instance.ReplaceCode(result);
			result = PhotosIdTemplate.Instance.ReplaceCode(result);
			result = PhotosTagTemplate.Instance.ReplaceCode(result);
			return result;
		}
	}
}