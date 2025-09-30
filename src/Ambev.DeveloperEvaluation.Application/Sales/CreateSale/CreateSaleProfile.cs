namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(x => x.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<SaleItemCommand, SaleItem>();

        CreateMap<Sale, CreateSaleResult>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id));
    }
}

