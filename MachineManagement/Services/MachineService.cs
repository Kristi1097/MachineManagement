using MachineManagement.Models;
using MachineManagement.Repositories;


namespace MachineManagement.Services
{
    public class MachineService:IMachineService
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
      /*  public async Task<int> GetByIdWithFailuresAsync(Machine machine)
        {
            var existing = await _repo.GetByNameAsync(machine.Name);
            if (existing != null)
                throw new Exception("Machine with the same name already exists.");
            return await _repo.AddAsync(machine);
        }*/
    }
}
