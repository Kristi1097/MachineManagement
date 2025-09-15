using MachineManagement.Models;

namespace MachineManagement.Repositories
{
    public interface IMachineRepository
    {
        Task<int> AddAsync(Machine machine);
        Task DeleteAsync(int id);
        Task<IEnumerable<Machine>> GetAllAsync();
        Task<Machine?> GetByIdAsync(int id);
        Task<Machine?> GetByIdWithFailuresAsync(int id);
        Task<Machine?> GetByNameAsync(string name);
        Task UpdateAsync(int id, Machine machine);
    }
}