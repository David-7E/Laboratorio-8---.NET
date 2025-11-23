using LAB8_David_Belizario.DTOs;
using LAB8_David_Belizario.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LAB8_David_Belizario.Controllers;

[ApiController]
[Route("api/clients")]
[Tags("Clients")]
public class ClientReportsController : ControllerBase
{
    private readonly IClientQueryService _clientQueries;

    public ClientReportsController(IClientQueryService clientQueries)
    {
        _clientQueries = clientQueries;
    }

    [HttpGet("by-name")]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetClientsByName([FromQuery] string term, CancellationToken cancellationToken = default)
    {
        var result = await _clientQueries.SearchByNameAsync(term, cancellationToken);
        return Ok(result);
    }

    [HttpGet("with-most-orders")]
    public async Task<ActionResult<IReadOnlyList<ClientOrderCountDto>>> GetClientsWithMostOrders(CancellationToken cancellationToken = default)
    {
        var result = await _clientQueries.GetClientsWithMostOrdersAsync(cancellationToken);
        return Ok(result);
    }

    // Paso 2
    [HttpGet("with-orders")]
    public async Task<ActionResult<IReadOnlyList<ClientWithOrdersDto>>> GetClientsWithOrders(CancellationToken cancellationToken = default)
    {
        var result = await _clientQueries.GetClientsWithOrdersAsync(cancellationToken);
        return Ok(result);
    }

    // Paso 4
    [HttpGet("with-product-totals")]
    public async Task<ActionResult<IReadOnlyList<ClientProductTotalDto>>> GetClientsWithProductTotals(CancellationToken cancellationToken = default)
    {
        var result = await _clientQueries.GetClientsWithProductTotalsAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("by-product/{productId:int}")]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetClientsByProduct(int productId, CancellationToken cancellationToken = default)
    {
        var result = await _clientQueries.GetClientsByPurchasedProductAsync(productId, cancellationToken);
        return Ok(result);
    }
}
