
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public SaleItem(Guid id, string productId, int quantity, decimal unitPrice)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        ApplyBusinessRulesAndCalculate(quantity);
    }
    public SaleItem(string productId, int quantity, decimal unitPrice)
    {
        
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        ApplyBusinessRulesAndCalculate(quantity);
    }
    public string ProductId { get; set; } = default!;

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }


    public decimal DiscountPercent { get; private set; } 
    public decimal TotalPrice { get; private set; }
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }

    public void Update(string productId, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;

        ApplyBusinessRulesAndCalculate(quantity);
    }
    private void ApplyBusinessRulesAndCalculate(int quantity)
    {
        ValidateQuantity(quantity);
        decimal discountPercent = 0m;
        
        if (Quantity >= 4 && Quantity < 10) 
            DiscountPercent = 10m;
        if (Quantity >= 10 && Quantity <= 20) 
            DiscountPercent = 20m;
        
        var lineGross = UnitPrice * Quantity;
        var lineNet = Math.Round(lineGross * (1 - discountPercent / 100m), 2);
        
        TotalPrice = lineNet;
    }

    private void ValidateQuantity(int q)
    {
        if (q > 20) throw new InvalidOperationException("Quantidade máxima por produto é 20.");
        if (q <= 0) throw new InvalidOperationException("Quantidade deve ser maior que zero.");
    }
}
