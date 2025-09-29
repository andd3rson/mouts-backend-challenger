namespace Ambev.DeveloperEvaluation.Unit.Application.SalesTests;
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _repo = Substitute.For<ISaleRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly ILogger<CreateSaleHandler> _logger = Substitute.For<ILogger<CreateSaleHandler>>();
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _handler = new CreateSaleHandler(_repo, _mapper, _logger);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreateSaleResult()
    {
        // Arrange
        var command = SaleHandlerTestData.ValidCreateSaleCommand();
        var saleEntity = new Sale
        {
            Id = Guid.NewGuid(),
            CustomerId = command.CustomerId,
            BranchId = command.BranchId           
        };
        var expected = new CreateSaleResult { Id = saleEntity.Id };

        _mapper.Map<Sale>(command).Returns(saleEntity);
        _repo.CreateAsync(saleEntity, Arg.Any<CancellationToken>())
             .Returns(saleEntity);
        _mapper.Map<CreateSaleResult>(saleEntity).Returns(expected);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(saleEntity.Id);
        await _repo.Received(1).CreateAsync(saleEntity, Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<Sale>(command);
        _mapper.Received(1).Map<CreateSaleResult>(saleEntity);
        _logger.Received(1).LogInformation("sale was created");
    }

    [Fact]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var invalid = new CreateSaleCommand();

        // Act
        Func<Task> act = () => _handler.Handle(invalid, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
        await _repo.DidNotReceiveWithAnyArgs().CreateAsync(default, default);
    }
}
