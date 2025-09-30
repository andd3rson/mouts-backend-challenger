using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact]
    public void Given_ActivatedSale_When_True_Then_ShouldBeCancelled()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var expected = true;

        // Act
        sale.CancelSale(expected);

        // Assert
        sale.Cancelled.Should().BeTrue();
    }

    [Fact]
    public void Given_ActivatedSale_When_UpdateBranchIdCustomer_Then_ShouldBeUpdated()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        const string branchId = "IPC-002";
        const string customerId = "C-ID001";
        const bool cancelled = false;
        
        // Act
        sale.UpdateSale(branchId, customerId, cancelled, sale.Items);

        // Assert
        sale.BranchId.Should().Be(branchId);
        sale.CustomerId.Should().Be(customerId);
        sale.Cancelled.Should().BeFalse();

        sale.Items.Should().NotBeNull();
        
    }
}
