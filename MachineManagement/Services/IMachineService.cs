using MachineManagement.Models;

namespace MachineManagement.Services
{
    public interface IMachineService
    {
        Task<int> AddMachineAsync(Machine machine);
    }
}