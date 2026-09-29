using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using MyFirstUnitTest.DTO;
using MyFirstUnitTest.Utils;

namespace MyFirstUnitTest.Repositories;

public class OrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(string connectionString = "Data Source=testdatabase.db")
    {
        _connectionString = connectionString;
    }

    private IDbConnection GetConnection() => new SqliteConnection(_connectionString);

    public async Task<OrderDTO?> GetOrderByIdAndUserIdAsync(int orderId, int userId)
    {
        using var connection = GetConnection();
        return await connection.QueryFirstOrDefaultAsync<OrderDTO>(
            SqlQueries.GetOrderByIdAndUserId,
            new { OrderId = orderId, UserId = userId });
    }
}