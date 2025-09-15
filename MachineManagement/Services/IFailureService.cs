using MachineManagement.Models;

namespace MachineManagement.Services
{
    public interface IFailureService
    {
        Task<int> AddFailureAsync(Failure failure);
        Task UpdateAsync(Failure failure);
        Task<Failure?> GetByIdAsync(int id);
        Task<IEnumerable<Failure>> GetAllAsync();
        Task ChangeStatusAsync(int id, bool status);
        Task DeleteFailureAsync(int id);
        Task<IEnumerable<Failure>> GetPagedSortedAsync(int offset, int limit);
    }
    
       
        
     
    
    }