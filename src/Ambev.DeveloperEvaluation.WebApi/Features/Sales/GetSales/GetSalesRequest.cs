namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

public class GetSalesRequest
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public string? Search { get; set; }
}
