using LAB8_David_Belizario.DTOs;
using LAB8_David_Belizario.Models;
using LAB8_David_Belizario.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using DbContext = LAB8_David_Belizario.Data.DbContext;

namespace LAB8_David_Belizario.Repositories;

public sealed class OrderRepository : ReadRepository<Order>, IOrderRepository
{
    public OrderRepository(DbContext context) : base(context)
    {
    }
    
    public async Task<OrderWithDetailsDto?> GetOrderWithProductDetailsAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var order = await Set
            .Include(o => o.Orderdetails)
                .ThenInclude(detail => detail.Product)
            .Include(o => o.Client)
            .AsNoTracking() // AsNoTracking() para eficiencia - Paso 3
            .FirstOrDefaultAsync(o => o.OrderId == orderId, cancellationToken);

        if (order is null)
        {
            return null;
        }

        return new OrderWithDetailsDto(
            order.OrderId,
            order.OrderDate,
            order.Client.Name,
            order.Orderdetails
                .OrderBy(detail => detail.OrderDetailId)
                .Select(detail => new OrderProductDetailDto(detail.Product.Name, detail.Quantity))
                .ToList());
    }
    
    public async Task<IReadOnlyList<SalesByClientDto>> GetSalesByClientAsync(CancellationToken cancellationToken = default)
    {
        return await Set
            .Include(o => o.Orderdetails)
                .ThenInclude(detail => detail.Product)
            .Include(o => o.Client)
            .AsNoTracking() // AsNoTracking() para eficiencia - Paso 5
            .GroupBy(order => new { order.ClientId, order.Client.Name })
            .Select(group => new SalesByClientDto(
                group.Key.Name,
                group.Sum(order => order.Orderdetails.Sum(detail => detail.Quantity * detail.Product.Price))
            ))
            .OrderByDescending(result => result.TotalSales)
            .ToListAsync(cancellationToken);
    }
}
