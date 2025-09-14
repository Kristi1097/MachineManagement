using MachineManagement.Repositories;

namespace MachineManagement.Services
{
    public class FailureService:IFailureService
    {
        private readonly IFailureRepository _repo;

        public FailureService(IFailureRepository repo)
        {
            _repo = repo;
        }
    }
}
