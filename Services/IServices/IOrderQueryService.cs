using LAB8_David_Belizario.DTOs;

namespace LAB8_David_Belizario.Services.IServices;

public interface IOrderQueryService
{
    Task<IReadOnlyList<OrderProductDetailDto>> GetOrderDetailsAsync(int orderId, CancellationToken cancellationToken = default);
    Task<int> GetTotalQuantityByOrderAsync(int orderId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<OrderSummaryDto>> GetOrdersAfterDateAsync(DateTime fromDate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<OrderWithDetailsDto>> GetOrdersWithDetailsAsync(CancellationToken cancellationToken = default);
}
