using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Areas.Admin.Models
{
	public class EditPostModel : PostModel
	{
		[Required]
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
	}
}