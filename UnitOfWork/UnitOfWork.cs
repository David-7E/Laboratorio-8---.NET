using System.Threading;
using System.Threading.Tasks;
using LAB8_David_Belizario.Repositories;
using LAB8_David_Belizario.Repositories.IRepositories;
using DbContext = LAB8_David_Belizario.Data.DbContext;
using IUnitOfWorkContract = LAB8_David_Belizario.UnitOfWork.IUnitOfWork.IUnitOfWork;

namespace LAB8_David_Belizario.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWorkContract
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
        Clients = new ClientRepository(_context);
        Products = new ProductRepository(_context);
        Orders = new OrderRepository(_context);
        OrderDetails = new OrderDetailRepository(_context);
    }

    public IClientRepository Clients { get; }
    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    public IOrderDetailRepository OrderDetails { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);

    public ValueTask DisposeAsync() => _context.DisposeAsync();
}
