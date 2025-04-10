using AralytiksTest2.Models;

namespace AralytiksTest2.Contracts
{
    public interface IPostRepository : IRespository<int, Post>
    {
        Task<Post?> GetWithUserAsync(int id, CancellationToken cancellationToken = default);
    }
}
