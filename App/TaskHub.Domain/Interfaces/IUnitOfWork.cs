namespace TaskHub.Domain.Interfaces;

public interface IUnitOfWork
{
    public Task BeginTransactionAsync();

    public Task CommitAsync();

    public Task RollbackAsync();

    public Task<int> SaveChagesAsync();

    public void Dispose();
}