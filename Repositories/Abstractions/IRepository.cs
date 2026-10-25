namespace blog_backend.Repositories.Abstractions;

public interface IRepository<TEntity, in TKey>
{
    Task<TEntity?> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> FindAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}
