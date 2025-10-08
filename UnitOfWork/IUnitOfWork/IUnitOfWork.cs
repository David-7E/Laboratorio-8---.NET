using LAB8_David_Belizario.Repositories.IRepositories;

namespace LAB8_David_Belizario.UnitOfWork.IUnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IClientRepository Clients { get; }
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    IOrderDetailRepository OrderDetails { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
