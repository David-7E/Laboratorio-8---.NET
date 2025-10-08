using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LAB8_David_Belizario.DTOs;
using LAB8_David_Belizario.Services.IServices;
using LAB8_David_Belizario.UnitOfWork.IUnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LAB8_David_Belizario.Services;

public sealed class ClientQueryService : IClientQueryService
{
    private readonly IUnitOfWork _unitOfWork;

    public ClientQueryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ClientDto>> SearchByNameAsync(string term, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return Array.Empty<ClientDto>();
        }

        var pattern = $"%{term.Trim()}%";

        return await _unitOfWork.Clients
            .Query()
            .Where(client => EF.Functions.Like(client.Name, pattern))
            .OrderBy(client => client.Name)
            .Select(client => new ClientDto(client.ClientId, client.Name, client.Email))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ClientOrderCountDto>> GetClientsWithMostOrdersAsync(CancellationToken cancellationToken = default)
    {
        var counts = await (from client in _unitOfWork.Clients.Query()
                            join order in _unitOfWork.Orders.Query()
                                on client.ClientId equals order.ClientId into clientOrders
                            let orderCount = clientOrders.Count()
                            orderby orderCount descending, client.Name
                            select new ClientOrderCountDto(client.ClientId, client.Name, orderCount))
            .ToListAsync(cancellationToken);

        if (counts.Count == 0)
        {
            return Array.Empty<ClientOrderCountDto>();
        }

        var maxOrders = counts[0].OrderCount;
        if (maxOrders == 0)
        {
            return Array.Empty<ClientOrderCountDto>();
        }

        return counts.Where(result => result.OrderCount == maxOrders).ToList();
    }

    public async Task<IReadOnlyList<ClientDto>> GetClientsByPurchasedProductAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.OrderDetails
            .Query()
            .Where(detail => detail.ProductId == productId)
            .GroupBy(detail => new { detail.Order.Client.ClientId, detail.Order.Client.Name, detail.Order.Client.Email })
            .OrderBy(group => group.Key.Name)
            .Select(group => new ClientDto(group.Key.ClientId, group.Key.Name, group.Key.Email))
            .ToListAsync(cancellationToken);
    }
}
