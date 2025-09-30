using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

public class CreateSaleProfile: Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>()
              .ForMember(x => x.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<SaleItemRequest, SaleItemCommand>();

        CreateMap<CreateSaleResult, CreateSaleResponse>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id));
    }
}
