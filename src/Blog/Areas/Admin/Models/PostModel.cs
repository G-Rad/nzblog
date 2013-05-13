using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Areas.Admin.Models
{
	public class PostModel
	{
		[Required]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		[DataType(DataType.MultilineText)]
		public string Body { get; set; }

		public string Thumbnail { get; set; }

		public string Url { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime DatePublished { get; set; }

		public bool IsPublished { get; set; }

		public string MetaDescription { get; set; }

		public string MetaImageUrl { get; set; }
	}
}