using AralytiksTest2.Contracts;
using AralytiksTest2.Models;

namespace AralytiksTest2.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext Context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.Context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
