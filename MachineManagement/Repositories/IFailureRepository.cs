using MachineManagement.Models;

namespace MachineManagement.Repositories
{
    public interface IFailureRepository
    {
        Task<int> AddAsync(Failure failure);
        Task DeleteAsync(int id);
        Task<IEnumerable<Failure>> GetAllAsync();
        Task<Failure?> GetByIdAsync(int id);
        Task<IEnumerable<Failure>> GetPagedSortedAsync(int offset, int limit);
        Task<bool> HasActiveFailure(int machineId);
        Task UpdateAsync(Failure failure);
    }
}