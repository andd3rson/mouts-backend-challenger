using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SalesRepository : RepositoryBase<Sale>, ISaleRepository
{
    private readonly DefaultContext _context;
    public SalesRepository(DefaultContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Sale> GetByIdAsync(Guid id, CancellationToken cancelation)
        => await _context.Sales.AsNoTracking()
        .Include(x => x.Items)
        .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<List<Sale>> GetFilteredAndPagedItems(int pageIndex, int pageSize, string? search, CancellationToken cancellationToken)
    {
        return await _context.Sales
                    .AsNoTracking()
                    .Include(x=>x.Items)
                    .Where(x=> 
                        EF.Functions.Like(x.CustomerId, $"%{search}%")
                        )
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)                   
                    .ToListAsync(cancellationToken);

       
    }

    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Sales.CountAsync(cancellationToken);
    }

    public new async Task<bool> UpdateAsync(Sale sale, CancellationToken cancellationToken)
    {
        var tracked = await _context.Sales
        .Include(x => x.Items)
        .FirstOrDefaultAsync(x => x.Id == sale.Id, cancellationToken);

        if (tracked == null)
            return false;

        tracked.UpdateSale(sale.BranchId, sale.CustomerId, sale.Cancelled, sale.Items);


        var incomingIds = sale.Items.Select(i => i.Id).ToList();

        var toRemove = tracked.Items.Where(i => !incomingIds.Contains(i.Id)).ToList();
        foreach (var item in toRemove)
        {
            _context.SaleItems.Remove(item);
        }

        foreach (var incoming in sale.Items)
        {
            var existing = tracked.Items.FirstOrDefault(i => i.Id == incoming.Id);
            if (existing != null)
            {
                existing.Update(incoming.ProductId, incoming.Quantity, incoming.UnitPrice);
            }
            else
            {
                tracked.Items.Add(incoming);
            }
        }

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

}
