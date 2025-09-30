
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Sale entity
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Sales Number
    /// </summary>
    public int SaleNumber { get; set; } = default!; 
    
    /// <summary>
    /// Customer ID .
    /// </summary>
    public string CustomerId { get; set; } = default!;

    /// <summary>
    /// Branch where the sale was made
    /// </summary>
    public string BranchId { get; set; } = default!;

    /// <summary>
    /// Total sales amount
    /// </summary>
    public decimal TotalAmount { get; set; }

    private List<SaleItem> _items = new();
    
    /// <summary>
    /// Items related to the sales.
    /// </summary>
    public List<SaleItem> Items {
        get => _items;
        set
        {
            _items = value ?? new List<SaleItem>();
            TotalAmount = _items.Sum(x => x.TotalPrice);
        }
    }

    /// <summary>
    /// Sales is actived or has been cancelled. Cancelled/Not Cancelled
    /// </summary>
    public bool Cancelled { get; set; } = false;

    public void CancelSale(bool cancelled)
    {
        Cancelled = cancelled;
    }
    public void UpdateSale(string brancId, string customerId, bool cancelled, List<SaleItem> items)
    {
        BranchId = brancId;
        CustomerId = customerId;
        Cancelled = cancelled;
        Items = items;
    }
    


}
