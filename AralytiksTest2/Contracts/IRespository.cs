using System.Linq.Expressions;

namespace AralytiksTest2.Contracts
{
    public interface IRespository<in TId, TEntity>
        where TEntity : class
    {
        Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default);
        Task<(IEnumerable<TEntity>? Entities, int TotalCount)> GetAllWithPagingationAsync(int page, int pageSize);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
