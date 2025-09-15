using Dapper;
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
        public async Task<int> AddAsync(Failure failure)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            var sql = @"INSERT INTO Failures 
                   (MachineId, Name, Priority, StartTime, EndTime, Description, Status)
                   VALUES (@MachineId, @Name, @Priority, @StartTime, @EndTime, @Description, @Status)
                   RETURNING Id;";
            return await conn.ExecuteScalarAsync<int>(sql, failure);
        }

        public async Task<Failure?> GetByIdAsync(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Failures WHERE Id=@Id;";
            return await conn.QueryFirstOrDefaultAsync<Failure>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Failure>> GetAllAsync()
        {
            using var conn = new NpgsqlConnection(_connectionString);
            return await conn.QueryAsync<Failure>("SELECT * FROM Failures;");
        }

        public async Task UpdateAsync(Failure failure)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            var sql = @"UPDATE Failures 
                   SET Name=@Name, Priority=@Priority, StartTime=@StartTime, EndTime=@EndTime, 
                       Description=@Description, Status=@Status
                   WHERE Id=@Id;";
            await conn.ExecuteAsync(sql, failure);
        }
      
        public async Task DeleteAsync(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.ExecuteAsync("DELETE FROM Failures WHERE Id=@Id;", new { Id = id });
        }
        public async Task<IEnumerable<Failure>> GetPagedSortedAsync(int offset, int limit)
        {
            using var conn = new NpgsqlConnection(_connectionString);

            var sql = @"
            SELECT * FROM Failures
            ORDER BY 
                CASE Priority 
                    WHEN 'Low' THEN 1
                    WHEN 'Medium' THEN 2
                    WHEN 'High' THEN 3
                END ASC,
                StartTime DESC
            OFFSET @Offset LIMIT @Limit;";

            return await conn.QueryAsync<Failure>(sql, new { Offset = offset, Limit = limit });
        }
        public async Task<bool> HasActiveFailure(int machineId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            var sql = "SELECT COUNT(*) FROM Failures WHERE MachineId=@MachineId AND Status=false;";
            var count = await conn.ExecuteScalarAsync<int>(sql, new { MachineId = machineId });
            return count > 0;
        }

    }
}
