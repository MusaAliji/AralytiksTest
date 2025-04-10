using AralytiksTest2.Contracts;
using AralytiksTest2.Models;
using Microsoft.EntityFrameworkCore;

namespace AralytiksTest2.Repositories
{
    public class PostRepository : BaseRepository<int, Post>, IPostRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Post> _dbSet;

        public PostRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Post>();
        }

        public async Task<Post?> GetWithUserAsync(int id, CancellationToken cancellationToken = default)
        {
            return await this._dbSet
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
