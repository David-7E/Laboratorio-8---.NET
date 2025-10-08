using LAB8_David_Belizario.DTOs;
using LAB8_David_Belizario.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LAB8_David_Belizario.Controllers;

[ApiController]
[Route("api/orders")]
[Tags("Orders")]
public class OrderReportsController : ControllerBase
{
    private readonly IOrderQueryService _orderQueries;

    public OrderReportsController(IOrderQueryService orderQueries)
    {
        _orderQueries = orderQueries;
    }

    [HttpGet("{orderId:int}/details")]
    public async Task<ActionResult<IReadOnlyList<OrderProductDetailDto>>> GetOrderDetails(int orderId, CancellationToken cancellationToken = default)
    {
        var result = await _orderQueries.GetOrderDetailsAsync(orderId, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{orderId:int}/total-quantity")]
    public async Task<ActionResult<int>> GetOrderTotalQuantity(int orderId, CancellationToken cancellationToken = default)
    {
        var total = await _orderQueries.GetTotalQuantityByOrderAsync(orderId, cancellationToken);
        return Ok(total);
    }

    [HttpGet("after")]
    public async Task<ActionResult<IReadOnlyList<OrderSummaryDto>>> GetOrdersAfter([FromQuery] DateTime from, CancellationToken cancellationToken = default)
    {
        var result = await _orderQueries.GetOrdersAfterDateAsync(from, cancellationToken);
        return Ok(result);
    }

    [HttpGet("with-details")]
    public async Task<ActionResult<IReadOnlyList<OrderWithDetailsDto>>> GetOrdersWithDetails(CancellationToken cancellationToken = default)
    {
        var result = await _orderQueries.GetOrdersWithDetailsAsync(cancellationToken);
        return Ok(result);
    }
}
