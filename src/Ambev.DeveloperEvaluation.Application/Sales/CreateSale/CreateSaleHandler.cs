
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<CreateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var isValid = await validator.ValidateAsync(request, cancellationToken);
        if (!isValid.IsValid)
            throw new ValidationException(isValid.Errors);

        var sale = _mapper.Map<Sale>(request);
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);
        _logger.LogInformation("sale was created");
        return result;

    }
}

