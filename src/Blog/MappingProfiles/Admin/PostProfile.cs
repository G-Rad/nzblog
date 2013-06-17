using AutoMapper;
using Core.Domain;
using Web.Areas.Admin.Models;

namespace Web.MappingProfiles.Admin
{
	public class PostProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<Post, EditPostModel>();

			Mapper.CreateMap<EditPostModel, Post>()
				.ForMember(post => post.Id, opt => opt.Ignore());

			Mapper.CreateMap<PostModel, Post>().ConstructUsingServiceLocator()
				.ForMember(post => post.Id, opt => opt.Ignore());
		}
	}
}