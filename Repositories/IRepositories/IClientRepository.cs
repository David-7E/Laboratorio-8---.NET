using System.Threading;
using LAB8_David_Belizario.DTOs;
using LAB8_David_Belizario.Models;

namespace LAB8_David_Belizario.Repositories.IRepositories;

public interface IClientRepository : IReadRepository<Client>
{
    Task<IReadOnlyList<ClientWithOrdersDto>> GetClientsWithOrdersAsync(CancellationToken cancellationToken = default); // Paso 2
    Task<IReadOnlyList<ClientProductTotalDto>> GetClientsWithProductTotalsAsync(CancellationToken cancellationToken = default); // Paso 4
}
