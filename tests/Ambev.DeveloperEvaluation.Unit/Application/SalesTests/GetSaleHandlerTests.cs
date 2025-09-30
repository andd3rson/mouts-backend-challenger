using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Unit.Application.SalesTests;

public class GetSaleHandlerTests
{
    private readonly ISaleRepository _repo = Substitute.For<ISaleRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly GetSaleHandler _handler;

    public GetSaleHandlerTests()
    {
        _handler = new GetSaleHandler(_repo, _mapper);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsResultsAndTotalCount()
    {
        // Arrange
        var cmd = SaleHandlerTestData.ValidGetSalesCommand();
        var sales = new List<Sale>
        {
            new Sale { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid().ToString()}
        };
        _repo.GetFilteredAndPagedItems(cmd.PageIndex, cmd.PageSize, cmd.Search, Arg.Any<CancellationToken>())
             .Returns(sales);
        _repo.GetTotalCountAsync().Returns(42);
        var expectedDtos = new List<GetSaleResult> { new GetSaleResult { Id = sales[0].Id } };
        _mapper.Map<List<GetSaleResult>>(sales).Returns(expectedDtos);

        // Act
        var (results, total) = await _handler.Handle(cmd, CancellationToken.None);

        // Assert
        results.Should().BeEquivalentTo(expectedDtos);
        total.Should().Be(42);
        await _repo.Received(1).GetFilteredAndPagedItems(cmd.PageIndex, cmd.PageSize, cmd.Search, Arg.Any<CancellationToken>());
        await _repo.Received(1).GetTotalCountAsync();
        _mapper.Received(1).Map<List<GetSaleResult>>(sales);
    }

}
