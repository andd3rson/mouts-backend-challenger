namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Sale>()
            .ForMember(x => x.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<SaleItemUpdateCommand, SaleItem>();

  
    }
}

