using Reddit.Dtos;
using Reddit.Models;

namespace Reddit.Mapper
{
    public interface IMapper
    {
        public Post toPost(CreatePostDto createPostDto);
        public Community toCommunity(CreateCommunityDto createCommunityDto);
    }
}
