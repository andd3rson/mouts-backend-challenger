using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSalesCommand : IRequest<(List<GetSaleResult>, int)>
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public string? Search { get; set; }
}
public class GetSaleResult
{
    public Guid Id { get; set; }
    public int SaleNumber { get; set; }


    public DateTimeOffset CreatedAt { get; set; }

    public string CustomerId { get; set; }

    public string BranchId { get; set; }


    public decimal TotalAmount { get; set; }


    public bool Cancelled { get; set; }


    public List<SaleItemResult> Items { get; set; }

}

public class SaleItemResult
{
    public Guid Id { get; set; }
    public string ProductId { get; set; } = default!;

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }


    public decimal DiscountPercent { get; private set; }
    public decimal TotalPrice { get; private set; }
}
