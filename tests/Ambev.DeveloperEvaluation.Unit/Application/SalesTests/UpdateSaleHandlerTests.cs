using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SalesTests;

public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _repo = Substitute.For<ISaleRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly ILogger<CreateSaleHandler> _logger = Substitute.For<ILogger<CreateSaleHandler>>();
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _handler = new UpdateSaleHandler(_repo, _mapper, _logger);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsUpdateSaleResult()
    {
        // Arrange
        var cmd = SaleHandlerTestData.ValidUpdateSaleCommand();
        var saleEntity = new Sale
        {
            Id = cmd.Id,
            CustomerId = cmd.CustomerId,
            BranchId = cmd.BranchId
           
        };

        _mapper.Map<Sale>(cmd).Returns(saleEntity);
        _repo.UpdateAsync(saleEntity, Arg.Any<CancellationToken>())
             .Returns(true);

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Assert
        result.Succeed.Should().BeTrue();
        _mapper.Received(1).Map<Sale>(cmd);
        await _repo.Received(1).UpdateAsync(saleEntity, Arg.Any<CancellationToken>());
        _logger.Received(1).LogInformation("[UpdateSaleResult] Succeed!");
    }

    [Fact]
    public async Task Handle_InvalidRequest_LogsErrorAndThrows()
    {
        // Arrange
        var cmd = new UpdateSaleCommand();

        // Act
        Func<Task> act = () => _handler.Handle(cmd, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
}
