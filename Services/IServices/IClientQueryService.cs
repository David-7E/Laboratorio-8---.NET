using LAB8_David_Belizario.DTOs;

namespace LAB8_David_Belizario.Services.IServices;

public interface IClientQueryService
{
    Task<IReadOnlyList<ClientDto>> SearchByNameAsync(string term, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ClientOrderCountDto>> GetClientsWithMostOrdersAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ClientDto>> GetClientsByPurchasedProductAsync(int productId, CancellationToken cancellationToken = default);
}
