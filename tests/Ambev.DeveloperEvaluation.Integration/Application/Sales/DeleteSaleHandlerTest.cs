using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Integration.Application.Sales;

public class DeleteSaleHandlerTest
{
    public ILogger<DeleteSaleHandler> Logger { get; }
    public DeleteSaleHandlerTest()
    {
        Logger = new LoggerFactory().CreateLogger<DeleteSaleHandler>();
    }

    [Fact]
    public async Task DeleteSale_ShouldBeDeleted()
    {
        // Arrange
        var context = new ConfigurationTests("SalesTestDb3");
        var salesService = new DeleteSaleHandler(context.SaleRepository, Logger);

        var newCommand = new Sale
        {
            CustomerId = Guid.NewGuid().ToString(),
            BranchId = Guid.NewGuid().ToString(),
            Items = new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid().ToString(), 2,50)
            }
        };
        var saved = await context.SaleRepository.CreateAsync(newCommand, CancellationToken.None);

        // Act
        var deleted = await salesService.Handle(new DeleteSaleCommand(saved.Id), CancellationToken.None);

        // Assert        
        deleted.Success.Should().BeTrue();

    }

    [Fact]
    public async Task DeleteSale_ShouldThrowAnException_When_ModelIsInvalid()
    {
        // Arrange
        var context = new ConfigurationTests("SalesTestD63");
        var salesHandler = new DeleteSaleHandler(context.SaleRepository, Logger);
        var command = new DeleteSaleCommand(Guid.Empty);

        // Act
        Func<Task> act = () => salesHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
