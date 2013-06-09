using System;
using System.Text.RegularExpressions;
using Core.Extensions;
using RazorTemplates.Core;

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

			var formattedBody = template.Render(null);
			return formattedBody;
		}

		private string ReplaceCodesToRazorSyntax()
		{
			var result = Body;
			var matches = Regex.Matches(Body, @"#{photos\(([\d,]*)\)}");
			for (int i = 0; i < matches.Count; i++)
			{
				result = result.Replace(matches[i].Value, "@PhotosTemplate.Instance.Render(\"" + matches[i].Groups[1] + "\")");
			}
			return result;
		}
	}
}