namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

public class CreateSaleRequest
{

    public string CustomerId { get; set; } = default!;

    public string BranchId { get; set; } = default!;
    public List<SaleItemRequest> Items { get; set; }
}
public record SaleItemRequest(string ProductId, int Quantity, decimal UnitPrice);

