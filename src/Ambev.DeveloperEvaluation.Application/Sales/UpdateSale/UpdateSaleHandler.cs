namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleValidator();
        var isValid = await validator.ValidateAsync(request, cancellationToken);
        if (!isValid.IsValid)
            throw new ValidationException(isValid.Errors);
      
        var createdSale = await _saleRepository.UpdateAsync(_mapper.Map<Sale>(request), cancellationToken);

        return new UpdateSaleResult { Succeed = createdSale };

    }
}

