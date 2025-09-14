using Dapper;
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
        public async Task<int> AddAsync(Machine machine)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            var sql = "INSERT INTO Machines (Name) VALUES (@Name) RETURNING Id;";
            return await conn.ExecuteScalarAsync<int>(sql, machine);
        }

        public async Task<Machine?> GetByIdAsync(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            var sql = @"SELECT * FROM Machines WHERE Id=@Id;";
            return await conn.QueryFirstOrDefaultAsync<Machine>(sql, new { Id = id });
        }
        public async Task<Machine?> GetByNameAsync(string name)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            var sql = @"SELECT * FROM Machines WHERE Name=@Name;";
            return await conn.QueryFirstOrDefaultAsync<Machine>(sql, new { Name = name });
        }

        public async Task<IEnumerable<Machine>> GetAllAsync()
        {
            using var conn = new NpgsqlConnection(_connectionString);
            return await conn.QueryAsync<Machine>("SELECT * FROM Machines;");
        }

    }
}