using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Integration.Application.Sales;

public class UpdateSaleHandlerTest
{
    public IMapper Mapper { get; }
    public ILogger<UpdateSaleHandler> Logger { get; }
    public UpdateSaleHandlerTest()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UpdateSaleProfile>();
        });
        Mapper = config.CreateMapper();
        Logger = new LoggerFactory().CreateLogger<UpdateSaleHandler>();
    }

    //[Fact]
    //public async Task UpdateSale_ShouldBeUpdated()
    //{
    //    // Arrange
    //    var context = new ConfigurationTests("SalesUodateTestDb");
    //    var salesService = new UpdateSaleHandler(context.SaleRepository, Mapper, Logger);
    //    var arrangeSale = new Sale()
    //    {
    //        CustomerId = Guid.NewGuid().ToString(),
    //        BranchId = Guid.NewGuid().ToString(),
    //        Items = new List<SaleItem>
    //        {
    //            new SaleItem(Guid.NewGuid().ToString(), 2,50)
    //        }
    //    };

    //    var arrangeCreated = await context.SaleRepository.CreateAsync(arrangeSale, CancellationToken.None);

    //    var newCommand = new UpdateSaleCommand
    //    {
    //        Id = arrangeCreated.Id,
    //        CustomerId = Guid.NewGuid().ToString(),
    //        BranchId = Guid.NewGuid().ToString(),
    //        Items = [
    //             new SaleItemUpdateCommand(Guid.NewGuid(), Guid.NewGuid().ToString(), 10, 10)
    //            ]
    //    };

    //    // Act
    //    var updated = await salesService.Handle(newCommand, CancellationToken.None);
    //    var salesUpdated = await context.SaleRepository.GetByIdAsync(arrangeSale.Id, CancellationToken.None);

    //    // Assert   
    //    salesUpdated.Items.First().TotalPrice.Should().Be(100);
    //}
    [Fact]
    public async Task Update_ShouldThrowAnException_When_ModelIsInvalid()
    {
        // Arrange
        var context = new ConfigurationTests("SalesTestDb1");
        var salesService = new UpdateSaleHandler(context.SaleRepository, Mapper, Logger);

        var newCommand = new UpdateSaleCommand
        {
            CustomerId = default,
            BranchId = Guid.NewGuid().ToString(),
            Items = new()
        };
        // Act
        Func<Task> act = () => salesService.Handle(newCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
