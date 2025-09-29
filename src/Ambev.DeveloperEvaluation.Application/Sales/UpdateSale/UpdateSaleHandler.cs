using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;
    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<CreateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleValidator();
        var isValid = await validator.ValidateAsync(request, cancellationToken);
        if (!isValid.IsValid)
        {
            _logger.LogError("[UpdateSaleCommand]: Invalid model");
            throw new ValidationException(isValid.Errors);
        }
      
        var updatedSale = await _saleRepository.UpdateAsync(_mapper.Map<Sale>(request), cancellationToken);
        _logger.LogInformation("[UpdateSaleResult] Succeed!");
        return new UpdateSaleResult { Succeed = updatedSale };

    }
}

