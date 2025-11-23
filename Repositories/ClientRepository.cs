using LAB8_David_Belizario.DTOs;
using LAB8_David_Belizario.Models;
using LAB8_David_Belizario.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using DbContext = LAB8_David_Belizario.Data.DbContext;

namespace LAB8_David_Belizario.Repositories;

public sealed class ClientRepository : ReadRepository<Client>, IClientRepository
{
    public ClientRepository(DbContext context) : base(context)
    {
    }

    // Paso 2: Consulta proyectada a DTO
    public async Task<IReadOnlyList<ClientWithOrdersDto>> GetClientsWithOrdersAsync(CancellationToken cancellationToken = default)
    {
        return await Set
            .AsNoTracking() // AsNoTracking() para eficiencia - Paso 2
            .Where(client => client.Orders.Any())
            .OrderBy(client => client.Name)
            .Select(client => new ClientWithOrdersDto(
                client.ClientId,
                client.Name,
                client.Orders
                    .OrderBy(order => order.OrderDate)
                    .Select(order => new ClientOrderDto(order.OrderId, order.OrderDate))
                    .ToList()
            ))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ClientProductTotalDto>> GetClientsWithProductTotalsAsync(CancellationToken cancellationToken = default)
    {
        var clients = await Set
            .AsNoTracking() // AsNoTracking() para eficiencia - Paso 4
            .Select(client => new { client.ClientId, client.Name })
            .OrderBy(client => client.Name)
            .ToListAsync(cancellationToken);

        if (clients.Count == 0)
        {
            return Array.Empty<ClientProductTotalDto>();
        }
        
        var totals = await Context.Set<Orderdetail>()
            .AsNoTracking() // AsNoTracking() para eficiencia - Paso 4
            .GroupBy(detail => detail.Order.ClientId)
            .Select(group => new
            {
                ClientId = group.Key,
                TotalProducts = group.Sum(detail => detail.Quantity)
            })
            .ToListAsync(cancellationToken);

        var totalsLookup = totals.ToDictionary(item => item.ClientId, item => item.TotalProducts);

        return clients
            .Select(client => new ClientProductTotalDto(
                client.ClientId,
                client.Name,
                totalsLookup.TryGetValue(client.ClientId, out var total) ? total : 0))
            .ToList();
    }
}
