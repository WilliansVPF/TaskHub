using Microsoft.EntityFrameworkCore.Storage;
using TaskHub.Domain.Interfaces;
using TaskHub.Infrastructure.Contexts;

namespace TaskHub.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TaskHubContext _context;
    private IDbContextTransaction _transaction = null!;

    public UnitOfWork(TaskHubContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        await _transaction.CommitAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public async Task<int> SaveChagesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}