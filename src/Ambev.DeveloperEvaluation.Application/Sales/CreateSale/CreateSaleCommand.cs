namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public string CustomerId { get; set; } = default!;
    public string BranchId { get; set; } = default!;
    public List<SaleItemCommand> Items { get; set; }
}

public record SaleItemCommand(string ProductId, int Quantity, decimal UnitPrice);
