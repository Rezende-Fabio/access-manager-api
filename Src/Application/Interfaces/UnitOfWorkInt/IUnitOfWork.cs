namespace access_manager_api.Application.Interfaces.UnitOfWorkInt;

public interface IUnitOfWork
{
    Task ExecuteInTransactionAsync(Func<Task> operation);
    Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation);
}