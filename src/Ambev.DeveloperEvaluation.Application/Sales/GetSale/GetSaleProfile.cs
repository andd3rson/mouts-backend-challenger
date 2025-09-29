namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;


public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
       
        CreateMap<Sale, GetSaleResult>();
        CreateMap<SaleItem, SaleItemResult>();

    }
}
