namespace LAB8_David_Belizario.Repositories.IRepositories;

public interface IReadRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query();
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
