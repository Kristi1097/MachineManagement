using MachineManagement.Models;
using MachineManagement.Repositories;


namespace MachineManagement.Services
{
    public class MachineService: IMachineService
    {
        private readonly IMachineRepository _repo;

        public MachineService(IMachineRepository repo)
        {
            _repo = repo;
        }
        public async Task<int> AddMachineAsync(Machine machine)
        {
            var existing = await _repo.GetByNameAsync(machine.Name);
            if (existing != null)
                throw new Exception("Machine with the same name already exists.");
            return await _repo.AddAsync(machine);
        }
        public async Task<Machine?> GetByIdWithFailuresAsync(int id)
        {
            return await _repo.GetByIdWithFailuresAsync(id);
        }
        public async Task<IEnumerable<Machine?>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task DeleteMachineAsync(int id)
        {
            var machine = await _repo.GetByIdAsync(id);
            if (machine == null)
                throw new Exception("Machine not found.");

            await _repo.DeleteAsync(id);
        }

        public async Task UpdateMachineAsync(int id, Machine machine)
        {
            await _repo.UpdateAsync(id, machine);
        }

        public Task<Machine?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
