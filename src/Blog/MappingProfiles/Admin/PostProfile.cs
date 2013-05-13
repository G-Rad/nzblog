using AutoMapper;
using Core.Domain;
using Web.Areas.Admin.Models;

namespace Web.MappingProfiles.Admin
{
	public class PostProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<Post, PostModel>();

			Mapper.CreateMap<PostModel, Post>()
				.ForMember(post => post.Id, opt => opt.Ignore());
		}
	}
}