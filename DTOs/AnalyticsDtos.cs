using System;
using System.Collections.Generic;

namespace LAB8_David_Belizario.DTOs;

public record ClientDto(int Id, string Name, string Email);
public record ProductDto(int Id, string Name, string? Description, decimal Price);
public record OrderProductDetailDto(string ProductName, int Quantity);
public record OrderSummaryDto(int OrderId, DateTime OrderDate, int ClientId, string ClientName);
public record OrderWithDetailsDto(int OrderId, DateTime OrderDate, string ClientName, IReadOnlyCollection<OrderProductDetailDto> Details);
public record ClientOrderCountDto(int ClientId, string ClientName, int OrderCount);
public record ProductAveragePriceDto(decimal AveragePrice);
