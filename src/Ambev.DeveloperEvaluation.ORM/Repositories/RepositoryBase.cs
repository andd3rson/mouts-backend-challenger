
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IRepositoryBase - Generic type to reuse entity framework core.
/// </summary>
public abstract class RepositoryBase<TEntity> where TEntity : BaseEntity
{
    private readonly DefaultContext _context;
    private readonly DbSet<TEntity> _db;

    /// <summary>
    /// Initializes a new instance of Repository Base
    /// </summary>
    /// <param name="context">The database context</param>
    public RepositoryBase(DefaultContext context)
    {
        _context = context;
        _db = _context.Set<TEntity>();
    }

    /// <summary>
    /// Creates a new TEntity in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created TEntity</returns>
    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _db.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    /// <summary>
    /// Retrieves a TEntity by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the TEntity was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return false;

        _db.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _db.Update(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
