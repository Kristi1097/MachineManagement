using MachineManagement.Models;
using Npgsql;


namespace MachineManagement.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly string _connectionString;
        public MachineRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

    }
}