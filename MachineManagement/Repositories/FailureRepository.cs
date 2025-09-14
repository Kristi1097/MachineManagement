using MachineManagement.Models;
using Npgsql;

namespace MachineManagement.Repositories
{
    public class FailureRepository : IFailureRepository
    {
        private readonly string _connectionString;
        public FailureRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
    }
}
