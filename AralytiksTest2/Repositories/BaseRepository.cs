using System.Linq.Expressions;
using AralytiksTest2.Contracts;
using AralytiksTest2.Models;
using Microsoft.EntityFrameworkCore;

namespace AralytiksTest2.Repositories
{
    public abstract class BaseRepository<TId, TEntity> : IRespository<TId, TEntity>
        where TEntity : class
    {
        internal readonly DbSet<TEntity> DbSet;
        protected readonly ApplicationDbContext Context;

        protected BaseRepository(ApplicationDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await this.DbSet.AddAsync(entity, cancellationToken);
            return await Task.FromResult(entity);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.Where(predicate);
        }

        public async Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await this.DbSet.FindAsync(id, cancellationToken);
        }

        public async Task<(IEnumerable<TEntity>? Entities, int TotalCount)> GetAllWithPagingationAsync(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            var query = this.DbSet.AsQueryable();

            int totalCount = await query.CountAsync();

            var users = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (this.Context.Entry(entity).State == EntityState.Detached)
                this.DbSet.Attach(entity);

            this.DbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            this.DbSet.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
