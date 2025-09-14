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
    }
}
