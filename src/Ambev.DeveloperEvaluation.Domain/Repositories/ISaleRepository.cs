
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);
    Task<List<Sale>> GetFilteredAndPagedItems(int pageIndex, int pageSize, string? search, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Sale sale, CancellationToken cancellationToken);
    Task<Sale> GetByIdAsync(Guid id, CancellationToken cancelation);
}
