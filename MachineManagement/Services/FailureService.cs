using MachineManagement.Models;
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
        public async Task<int> AddFailureAsync(Failure failure)
        {
            if (string.IsNullOrWhiteSpace(failure.Description))
                throw new Exception("Failure must have a description.");

            var active = await _repo.HasActiveFailure(failure.MachineId);
            if (active)
                throw new Exception("Machine already has an active failure.");

            return await _repo.AddAsync(failure);
        }
        public async Task<Failure> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Failure>> GetPagedSortedAsync(int offset, int limit)
        {
            return await _repo.GetPagedSortedAsync(offset, limit);
        }
        public async Task<IEnumerable<Failure?>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
        public async Task ChangeStatusAsync(int id, bool status)
        {
            var failure = await _repo.GetByIdAsync(id);
            if (failure == null)
                throw new Exception("Failure not found.");

            failure.Status = status;
            if (status && failure.EndTime == null)
                failure.EndTime = DateTime.UtcNow;

            await _repo.UpdateAsync(failure);
        }
        public async Task DeleteFailureAsync(int id)
        {
            var failure = await _repo.GetByIdAsync(id);
            if (failure == null)
                throw new Exception("Failure not found.");

            await _repo.DeleteAsync(id);
        }

        public Task UpdateAsync(Failure failure)
        {
            throw new NotImplementedException();
        }
    }
}
