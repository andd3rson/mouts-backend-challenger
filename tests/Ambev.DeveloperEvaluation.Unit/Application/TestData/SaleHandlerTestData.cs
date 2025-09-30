using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class SaleHandlerTestData
{

    private static readonly Faker<SaleItemCommand> _itemFaker = new Faker<SaleItemCommand>()
        .CustomInstantiator(f =>
            new SaleItemCommand(
                ProductId: f.Random.Guid().ToString(),
                Quantity: f.Random.Int(1, 10),
                UnitPrice: f.Finance.Amount(1M, 500M)
            )
        );

    private static readonly Faker<SaleItemUpdateCommand> _itemUpdateFaker = new Faker<SaleItemUpdateCommand>()
    .CustomInstantiator(f =>
        new SaleItemUpdateCommand(
            Id: f.Random.Guid(),
            ProductId: f.Random.Guid().ToString(),
            Quantity: f.Random.Int(1, 10),
            UnitPrice: f.Finance.Amount(1M, 500M)
        )
    );

    private static List<SaleItemCommand> GenerateItems()
    {
        var count = new Faker().Random.Int(1, 5);
        return _itemFaker.Generate(count);
    }
    private static List<SaleItemUpdateCommand> GenerateUpdateItems()
    {
        var count = new Faker().Random.Int(1, 5);
        return _itemUpdateFaker.Generate(count);
    }

    // Faker for CreateSaleCommand
    private static readonly Faker<CreateSaleCommand> _createSaleFaker = new Faker<CreateSaleCommand>()
        .RuleFor(c => c.CustomerId, f => f.Random.Guid().ToString())
        .RuleFor(c => c.BranchId, f => f.Random.Guid().ToString())
        .RuleFor(c => c.Items, f => GenerateItems());

    // Faker for UpdateSaleCommand
    private static readonly Faker<UpdateSaleCommand> _updateSaleFaker = new Faker<UpdateSaleCommand>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.CustomerId, f => f.Random.Guid().ToString())
        .RuleFor(c => c.BranchId, f => f.Random.Guid().ToString())
        .RuleFor(c => c.Items, f => GenerateUpdateItems());

    // Faker for GetSalesCommand remains unchanged
    private static readonly Faker<GetSalesCommand> _getSalesFaker = new Faker<GetSalesCommand>()
        .RuleFor(c => c.PageIndex, f => f.Random.Int(1, 5))
        .RuleFor(c => c.PageSize, f => f.Random.Int(5, 50))
        .RuleFor(c => c.Search, f => f.Lorem.Word());

    public static CreateSaleCommand ValidCreateSaleCommand() => _createSaleFaker.Generate();
    public static UpdateSaleCommand ValidUpdateSaleCommand() => _updateSaleFaker.Generate();
    public static DeleteSaleCommand ValidDeleteSaleCommand() => new DeleteSaleCommand(Guid.NewGuid());
    public static GetSalesCommand ValidGetSalesCommand() => _getSalesFaker.Generate();

}
