using LAB8_David_Belizario.DTOs;

namespace LAB8_David_Belizario.Services.IServices;

public interface IClientQueryService
{
    Task<IReadOnlyList<ClientDto>> SearchByNameAsync(string term, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ClientOrderCountDto>> GetClientsWithMostOrdersAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ClientWithOrdersDto>> GetClientsWithOrdersAsync(CancellationToken cancellationToken = default); // Paso 2
    Task<IReadOnlyList<ClientProductTotalDto>> GetClientsWithProductTotalsAsync(CancellationToken cancellationToken = default); // Paso 4
    Task<IReadOnlyList<ClientDto>> GetClientsByPurchasedProductAsync(int productId, CancellationToken cancellationToken = default);
}
