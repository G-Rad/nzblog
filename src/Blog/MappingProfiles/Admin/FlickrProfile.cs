using System.Linq;
using AutoMapper;
using Core.Domain;
using Web.Areas.Admin.Models.Flickr;

namespace Web.MappingProfiles.Admin
{
	public class FlickrProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<Flick, FlickrModel>()
				.ForMember(fm => fm.Tags, opt => opt.Ignore())
				.AfterMap((flick, model) =>
					{
						model.Tags = flick.Tags.Any()
										? flick.Tags.Aggregate((current, next) => string.Concat(current, ", ", next))
										: "";
					});
		}
	}
}