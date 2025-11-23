using System.Threading;
using LAB8_David_Belizario.DTOs;
using LAB8_David_Belizario.Models;

namespace LAB8_David_Belizario.Repositories.IRepositories;

public interface IOrderRepository : IReadRepository<Order>
{
    Task<OrderWithDetailsDto?> GetOrderWithProductDetailsAsync(int orderId, CancellationToken cancellationToken = default); // Paso 3
    Task<IReadOnlyList<SalesByClientDto>> GetSalesByClientAsync(CancellationToken cancellationToken = default); // Paso 5
}
