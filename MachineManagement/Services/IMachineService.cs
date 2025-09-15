using MachineManagement.Models;

namespace MachineManagement.Services
{
    public interface IMachineService
    {
        Task<int> AddMachineAsync(Machine machine);
        Task UpdateMachineAsync(int id, Machine machine);
        Task<Machine?> GetByIdAsync(int id);
        Task<Machine?> GetByIdWithFailuresAsync(int id);
        Task<IEnumerable<Machine>> GetAllAsync();
        Task DeleteMachineAsync(int id);

    }
}