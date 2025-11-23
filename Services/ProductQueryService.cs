using LAB8_David_Belizario.DTOs;
using LAB8_David_Belizario.Services.IServices;
using LAB8_David_Belizario.UnitOfWork.IUnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LAB8_David_Belizario.Services;

public sealed class ProductQueryService : IProductQueryService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductQueryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ProductDto>> GetProductsByPriceGreaterThanAsync(decimal minPrice, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Products
            .Query()
            .AsNoTracking() // AsNoTracking() para eficiencia - Paso 2
            .Where(product => product.Price > minPrice)
            .OrderBy(product => product.Price)
            .Select(product => new ProductDto(product.ProductId, product.Name, product.Description, product.Price))
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductDto?> GetMostExpensiveProductAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Products
            .Query()
            .AsNoTracking() // AsNoTracking() para eficiencia - Paso 2
            .OrderByDescending(product => product.Price)
            .Select(product => new ProductDto(product.ProductId, product.Name, product.Description, product.Price))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ProductAveragePriceDto> GetAverageProductPriceAsync(CancellationToken cancellationToken = default)
    {
        var average = await _unitOfWork.Products
            .Query()
            .AsNoTracking() // AsNoTracking() para eficiencia - Paso 2
            .Select(product => (decimal?)product.Price)
            .AverageAsync(cancellationToken);

        return new ProductAveragePriceDto(average ?? 0m);
    }

    public async Task<IReadOnlyList<ProductDto>> GetProductsWithoutDescriptionAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Products
            .Query()
            .AsNoTracking()
            .Where(product => product.Description == null || product.Description == string.Empty)
            .OrderBy(product => product.Name)
            .Select(product => new ProductDto(product.ProductId, product.Name, product.Description, product.Price))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProductDto>> GetProductsSoldToClientAsync(int clientId, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.OrderDetails
            .Query()
            .AsNoTracking()
            .Where(detail => detail.Order.ClientId == clientId)
            .GroupBy(detail => new { detail.Product.ProductId, detail.Product.Name, detail.Product.Description, detail.Product.Price })
            .OrderBy(group => group.Key.Name)
            .Select(group => new ProductDto(group.Key.ProductId, group.Key.Name, group.Key.Description, group.Key.Price))
            .ToListAsync(cancellationToken);
    }
}


//Comentario a probar para commit