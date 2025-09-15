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
        public async Task UpdateAsync(int id, Machine machine)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            var sql = @"UPDATE Machines 
                   SET Name=@Name
                   WHERE Id=@Id;";  
            await conn.ExecuteAsync(sql,machine);
        }
        public async Task<Machine?> GetByIdAsync(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            var sql = @"SELECT * FROM Machines WHERE Id=@Id;";
            return await conn.QueryFirstOrDefaultAsync<Machine>(sql, new { Id = id });
        }
        public async Task<Machine?> GetByIdWithFailuresAsync(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);

            var sql = @"
        SELECT m.Id, m.Name,
               f.Id, f.MachineId, f.Name, f.Priority, f.StartTime, f.EndTime, f.Description, f.Status
        FROM Machines m
        LEFT JOIN Failures f ON m.Id = f.MachineId
        WHERE m.Id = @Id;";

            var machineDict = new Dictionary<int, Machine>();

            var result = await conn.QueryAsync<Machine, Failure, Machine>(
                sql,
                (machine, failure) =>
                {
                    if (!machineDict.TryGetValue(machine.Id, out var currentMachine))
                    {
                        currentMachine = machine;
                        currentMachine.Failures = new List<Failure>();
                        machineDict.Add(machine.Id, currentMachine);
                    }

                    if (failure != null)
                    {
                        ((List<Failure>)currentMachine.Failures!).Add(failure);
                    }

                    return currentMachine;
                },
                new { Id = id }
            );

            var finalMachine = result.FirstOrDefault();

            if (finalMachine != null && finalMachine.Failures != null && finalMachine.Failures.Any(f => f.EndTime.HasValue))
            {
                var durations = finalMachine.Failures
                    .Where(f => f.EndTime.HasValue)
                    .Select(f => (f.EndTime!.Value - f.StartTime).TotalHours);

                finalMachine.AverageTimeFailures= durations.Any() ? durations.Average() : 0;
            }

            return finalMachine;
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
        public async Task DeleteAsync(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.ExecuteAsync("DELETE FROM Machines WHERE Id=@Id;", new { Id = id });
        }
    }
}