using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Integration.Application.Sales;

public class CreateSaleHandlerTest
{
    public IMapper Mapper { get; }
    public ILogger<CreateSaleHandler> Logger { get; }
    public CreateSaleHandlerTest()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateSaleProfile>(); 
        });
        Mapper = config.CreateMapper();
        Logger = new LoggerFactory().CreateLogger<CreateSaleHandler>();
    }

    [Fact]
    public async Task CreateSale_ShouldBeSaved()
    {
        // Arrange
        var context = new ConfigurationTests("SalesTestDb1");
        var salesService = new CreateSaleHandler(context.SaleRepository, Mapper, Logger);

        var newCommand = new CreateSaleCommand
        {
            CustomerId = Guid.NewGuid().ToString(),
            BranchId = Guid.NewGuid().ToString(),
            Items = new List<SaleItemCommand>
            {
                new SaleItemCommand(Guid.NewGuid().ToString(), 2,50)
            }
        };
        // Act
        var created = await salesService.Handle(newCommand, CancellationToken.None);
        var saved = await context.SaleRepository.GetByIdAsync(created.Id, CancellationToken.None);

        // Assert   
        saved.Items.First().TotalPrice.Should().Be(100);
    }
    [Fact]
    public async Task CreateSale_ShouldThrowAnException_When_ModelIsInvalid()
    {
        // Arrange
        var context = new ConfigurationTests("SalesTestDb1");
        var salesService = new CreateSaleHandler(context.SaleRepository, Mapper, Logger);

        var newCommand = new CreateSaleCommand
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
