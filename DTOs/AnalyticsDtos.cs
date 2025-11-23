namespace LAB8_David_Belizario.DTOs;

public record ClientDto(int Id, string Name, string Email);
public record ProductDto(int Id, string Name, string? Description, decimal Price);
public record OrderProductDetailDto(string ProductName, int Quantity);
public record OrderSummaryDto(int OrderId, DateTime OrderDate, int ClientId, string ClientName);
public record OrderWithDetailsDto(int OrderId, DateTime OrderDate, string ClientName, IReadOnlyCollection<OrderProductDetailDto> Details);
public record ClientOrderCountDto(int ClientId, string ClientName, int OrderCount);
public record ProductAveragePriceDto(decimal AveragePrice);

// Paso 2: DTOs para relacion Cliente-Pedidos
public record ClientOrderDto(int OrderId, DateTime OrderDate);
public record ClientWithOrdersDto(int ClientId, string ClientName, IReadOnlyCollection<ClientOrderDto> Orders);

// Paso 4: DTO para totales de productos por cliente
public record ClientProductTotalDto(
    int ClientId, 
    string ClientName, 
    int TotalProducts);

// Paso 5: DTO para ventas totales por cliente
public record SalesByClientDto(string ClientName, decimal TotalSales);
