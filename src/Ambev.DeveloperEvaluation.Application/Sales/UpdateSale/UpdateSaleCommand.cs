namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid Id { get; set; }
    public bool Cancelled { get; set; }
    /// <summary>
    /// Customer ID .
    /// </summary>
    public string CustomerId { get; set; } = default!;

    /// <summary>
    /// Branch where the sale was made
    /// </summary>
    public string BranchId { get; set; } = default!;

    /// <summary>
    /// Items related to the sales.
    /// </summary>
    public List<SaleItemUpdateCommand> Items { get; set; }

}

public record SaleItemUpdateCommand(Guid Id, string ProductId, int Quantity, decimal UnitPrice );
