using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SalesTests;
public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _repo = Substitute.For<ISaleRepository>();
    private readonly ILogger<DeleteSaleHandler> _logger = Substitute.For<ILogger<DeleteSaleHandler>>();
    private readonly DeleteSaleHandler _handler;

    public DeleteSaleHandlerTests()
    {
        _handler = new DeleteSaleHandler(_repo, _logger);
    }


    [Fact]
    public async Task Handle_NonExistingSale_LogsErrorAndThrowsKeyNotFound()
    {
        // Arrange
        var cmd = SaleHandlerTestData.ValidDeleteSaleCommand();
        _repo.DeleteAsync(cmd.Id, Arg.Any<CancellationToken>()).Returns(false);

        // Act
        Func<Task> act = () => _handler.Handle(cmd, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with ID {cmd.Id} not found");
        _logger.Received(1).LogError("[DeleteSaleCommand] id was not foung");
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var cmd = SaleHandlerTestData.ValidDeleteSaleCommand();
        _repo.DeleteAsync(cmd.Id, Arg.Any<CancellationToken>()).Returns(true);

        // Act
        var response = await _handler.Handle(cmd, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();
        await _repo.Received(1).DeleteAsync(cmd.Id, Arg.Any<CancellationToken>());
    }
}
