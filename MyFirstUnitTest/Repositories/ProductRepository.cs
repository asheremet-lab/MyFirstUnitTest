using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using MyFirstUnitTest.DTO;
using MyFirstUnitTest.Utils;

namespace MyFirstUnitTest.Repositories;

public class ProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(string connectionString = "Data Source=testdatabase.db")
    {
        _connectionString = connectionString;
    }

    private IDbConnection GetConnection() => new SqliteConnection(_connectionString);

    public async Task<ProductDTO?> GetProductByIdAsync(int targetId)
    {
        using var connection = GetConnection();
        return await connection.QueryFirstOrDefaultAsync<ProductDTO>(
            SqlQueries.GetProductById,
            new { Id = targetId });
    }

    public async Task<List<ProductDTO>> GetProductsByOrderIdAsync(int orderId)
    {
        using var connection = GetConnection();
        var result = await connection.QueryAsync<ProductDTO>(
            SqlQueries.GetProductsByOrderId,
            new { OrderId = orderId });
        return result.ToList();
    }
}