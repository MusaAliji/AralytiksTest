using AralytiksTest2.DTO;
using AralytiksTest2.Models;

namespace AralytiksTest2.Contracts
{
    public interface IPostService
    {
        Task<(IEnumerable<Post>? posts, int totalCount)> GetAllPostsAsync(int page, int pageSize);
        Task<Post?> GetPostByIdAsync(int id);
        Task<Post?> CreatePostAsync(PostDto post);
        Task<Post?> UpdatePostAsync(int id, PostDto post);
        Task<bool> DeletePostAsync(int id);
    }
}
