using LAB8_David_Belizario.DTOs;
using LAB8_David_Belizario.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LAB8_David_Belizario.Controllers;

[ApiController]
[Route("api/products")]
[Tags("Products")]
public class ProductReportsController : ControllerBase
{
    private readonly IProductQueryService _productQueries;

    public ProductReportsController(IProductQueryService productQueries)
    {
        _productQueries = productQueries;
    }

    [HttpGet("by-min-price")]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProductsByMinPrice([FromQuery] decimal minPrice, CancellationToken cancellationToken = default)
    {
        var result = await _productQueries.GetProductsByPriceGreaterThanAsync(minPrice, cancellationToken);
        return Ok(result);
    }

    [HttpGet("most-expensive")]
    public async Task<ActionResult<ProductDto>> GetMostExpensiveProduct(CancellationToken cancellationToken = default)
    {
        var product = await _productQueries.GetMostExpensiveProductAsync(cancellationToken);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpGet("average-price")]
    public async Task<ActionResult<ProductAveragePriceDto>> GetAverageProductPrice(CancellationToken cancellationToken = default)
    {
        var result = await _productQueries.GetAverageProductPriceAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("without-description")]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProductsWithoutDescription(CancellationToken cancellationToken = default)
    {
        var result = await _productQueries.GetProductsWithoutDescriptionAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("by-client/{clientId:int}")]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProductsSoldToClient(int clientId, CancellationToken cancellationToken = default)
    {
        var result = await _productQueries.GetProductsSoldToClientAsync(clientId, cancellationToken);
        return Ok(result);
    }
}
