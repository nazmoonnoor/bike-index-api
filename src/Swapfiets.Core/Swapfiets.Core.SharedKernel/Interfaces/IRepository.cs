using Swapfiets.Core.SharedKernel;

namespace Swapfiets.Core.SharedKernel.Interfaces
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(Guid id) where T : EntityBase;
        Task<List<T>> ListAsync<T>() where T : EntityBase;
        Task<T> AddAsync<T>(T entity) where T : EntityBase;
        Task UpdateAsync<T>(T entity) where T : EntityBase;
        Task DeleteAsync<T>(T entity) where T : EntityBase;
    }
}
