namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Handler for processing GetSalesCommand requests
/// </summary>
public class GetSaleHandler : IRequestHandler<GetSalesCommand, (List<GetSaleResult>, int)>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<(List<GetSaleResult>, int)> Handle(GetSalesCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository
            .GetFilteredAndPagedItems(request.PageIndex, request.PageSize, request.Search, cancellationToken);

        var totalCount = await _saleRepository.GetTotalCountAsync();
        return (_mapper.Map<List<GetSaleResult>>(sale), totalCount);
    }
}
