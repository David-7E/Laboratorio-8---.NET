using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LAB8_David_Belizario.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LAB8_David_Belizario.Repositories;

public class ReadRepository<TEntity> : IReadRepository<TEntity>
    where TEntity : class
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> Set;

    public ReadRepository(DbContext context)
    {
        Context = context;
        Set = Context.Set<TEntity>();
    }

    public IQueryable<TEntity> Query() => Set.AsNoTracking();

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Set.FindAsync(new object[] { id }, cancellationToken);
    }
}
