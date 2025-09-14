using MachineManagement.Models;

namespace MachineManagement.Repositories
{
    public interface IMachineRepository
    {
        Task<int> AddAsync(Machine machine);
        Task<IEnumerable<Machine>> GetAllAsync();
        Task<Machine?> GetByIdAsync(int id);
        Task<Machine?> GetByNameAsync(string name);
    }
}