using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSales;

public class UpdateSaleProfile: Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>()
              .ForMember(x => x.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<SaleItemUpdateRequest, SaleItemUpdateCommand>();
    }
}
