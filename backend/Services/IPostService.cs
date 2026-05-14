using RedditClone.Dtos;

namespace RedditClone.Services
{
    public interface IPostService
    {

        Task<ServiceResult<object>> AddPostAsync(CreatePostDto post, Guid userId);
        Task<ServiceResult<object>> RemovePostAsync(Guid id, Guid userId);
        Task<ServiceResult<List<PostDto>>> GetAllPostsAsync();
        Task<ServiceResult<PostDetailDto>> GetPostByIdAsync(Guid id);

    }
}
