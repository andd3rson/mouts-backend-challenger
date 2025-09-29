namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public class GetSalesResponse
{
    public Guid Id { get; set; }
    public int SaleNumber { get; set; }


    public DateTimeOffset CreatedAt { get; set; }

    public string CustomerId { get; set; }

    public string BranchId { get; set; }


    public decimal TotalAmount { get; set; }


    public bool Cancelled { get; set; }


    public List<SaleItemResponse> Items { get; set; }
}


public class SaleItemResponse
{
    public Guid Id { get; set; }
    public string ProductId { get; set; } = default!;

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }


    public decimal DiscountPercent { get; private set; }
    public decimal TotalPrice { get; private set; }
}
