namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSales;

public class UpdateRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.Id   )
           .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");

        RuleFor(x => x.BranchId)
            .NotEmpty().WithMessage("BranchId is required.");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("At least one item is required.")
            .Must(i => i.Any()).WithMessage("At least one item is required.");

        RuleForEach(x => x.Items).SetValidator(new SaleItemUpdateRequestValidator());
    }
}
public class SaleItemUpdateRequestValidator : AbstractValidator<SaleItemUpdateRequest>
{
    public SaleItemUpdateRequestValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .WithMessage("Id is required.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(20).WithMessage("Quantity cannot be greater than 20.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("UnitPrice must be greater than 0.");
    }
}