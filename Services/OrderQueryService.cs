using LAB8_David_Belizario.DTOs;
using LAB8_David_Belizario.Services.IServices;
using LAB8_David_Belizario.UnitOfWork.IUnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LAB8_David_Belizario.Services;

public sealed class OrderQueryService : IOrderQueryService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderQueryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<OrderProductDetailDto>> GetOrderDetailsAsync(int orderId, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.OrderDetails
            .Query()
            .AsNoTracking()
            .Where(detail => detail.OrderId == orderId)
            .OrderBy(detail => detail.OrderDetailId)
            .Select(detail => new OrderProductDetailDto(detail.Product.Name, detail.Quantity))
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetTotalQuantityByOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var total = await _unitOfWork.OrderDetails
            .Query()
            .AsNoTracking()
            .Where(detail => detail.OrderId == orderId)
            .Select(detail => (int?)detail.Quantity)
            .SumAsync(cancellationToken);

        return total ?? 0;
    }

    public async Task<IReadOnlyList<OrderSummaryDto>> GetOrdersAfterDateAsync(DateTime fromDate, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Orders
            .Query()
            .AsNoTracking() 
            .Where(order => order.OrderDate > fromDate)
            .OrderBy(order => order.OrderDate)
            .Select(order => new OrderSummaryDto(order.OrderId, order.OrderDate, order.ClientId, order.Client.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<OrderWithDetailsDto>> GetOrdersWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Orders
            .Query()
            .AsNoTracking()
            .OrderBy(order => order.OrderDate)
            .Select(order => new OrderWithDetailsDto(
                order.OrderId,
                order.OrderDate,
                order.Client.Name,
                order.Orderdetails
                    .OrderBy(detail => detail.OrderDetailId)
                    .Select(detail => new OrderProductDetailDto(detail.Product.Name, detail.Quantity))
                    .ToList()
            ))
            .ToListAsync(cancellationToken);
    }

    public Task<OrderWithDetailsDto?> GetOrderWithProductDetailsAsync(int orderId, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.Orders.GetOrderWithProductDetailsAsync(orderId, cancellationToken); // Paso 3
    }

    public Task<IReadOnlyList<SalesByClientDto>> GetSalesByClientAsync(CancellationToken cancellationToken = default)
    {
        return _unitOfWork.Orders.GetSalesByClientAsync(cancellationToken); // Paso 5
    }
}
