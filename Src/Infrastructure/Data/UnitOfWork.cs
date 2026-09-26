using access_manager_api.Application.Interfaces.UnitOfWorkInt;
using Microsoft.EntityFrameworkCore;

namespace access_manager_api.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task ExecuteInTransactionAsync(Func<Task> operation)
    {
        if (_dbContext.Database.CurrentTransaction is not null)
        {
            await operation();
            await _dbContext.SaveChangesAsync();
            return;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await operation();
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation)
    {
        if (_dbContext.Database.CurrentTransaction is not null)
        {
            var resultado = await operation();
            await _dbContext.SaveChangesAsync();
            return resultado;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var result = await operation();
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}