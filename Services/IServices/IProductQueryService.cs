using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LAB8_David_Belizario.DTOs;

namespace LAB8_David_Belizario.Services.IServices;

public interface IProductQueryService
{
    Task<IReadOnlyList<ProductDto>> GetProductsByPriceGreaterThanAsync(decimal minPrice, CancellationToken cancellationToken = default);
    Task<ProductDto?> GetMostExpensiveProductAsync(CancellationToken cancellationToken = default);
    Task<ProductAveragePriceDto> GetAverageProductPriceAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductDto>> GetProductsWithoutDescriptionAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductDto>> GetProductsSoldToClientAsync(int clientId, CancellationToken cancellationToken = default);
}
