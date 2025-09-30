using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleTestData
{
    private static readonly Faker<Sale> SalesFaker = new Faker<Sale>()
        .RuleFor(u =>u.BranchId, f=>f.Random.Guid().ToString())
        .RuleFor(u =>u.CustomerId, f=>f.Random.Guid().ToString())
        .RuleFor(u => u.SaleNumber, f => f.Random.Number(1, 999))
        .RuleFor(c => c.Items, f => GenerateSaleItems());
    private static List<SaleItem> GenerateSaleItems()
    {
        var count = new Faker().Random.Int(1, 5);
        return _itemSalesFaker.Generate(count);
    }
    private static readonly Faker<SaleItem> _itemSalesFaker = new Faker<SaleItem>()
    .CustomInstantiator(f =>
        new SaleItem(
            id: f.Random.Guid(),
            productId: f.Random.Guid().ToString(),
            quantity: f.Random.Int(1, 10),
            unitPrice: f.Finance.Amount(1M, 500M)
        )
    );

    public static Sale GenerateValidSale()
    {
        return SalesFaker.Generate();
    }
}
