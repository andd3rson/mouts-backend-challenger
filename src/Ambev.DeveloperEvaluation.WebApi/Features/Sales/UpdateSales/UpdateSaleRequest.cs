
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSales;

public class UpdateSaleRequest
{
    public string CustomerId { get; set; }
    public string BranchId { get; set; }

    public Guid Id { get; set; }
    public bool Cancelled { get; set; }
    public List<SaleItemUpdateRequest> Items { get; set; }
}
public record SaleItemUpdateRequest(Guid Id, string ProductId, int Quantity, decimal UnitPrice);

