using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using MyFirstUnitTest.DTO;
using MyFirstUnitTest.Utils;

namespace MyFirstUnitTest.Repositories;

public class CategoryRepository
{
    private readonly string _connectionString;

    public CategoryRepository(string connectionString = "Data Source=testdatabase.db")
    {
        _connectionString = connectionString;
    }

    private IDbConnection GetConnection() => new SqliteConnection(_connectionString);

    public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
    {
        using var connection = GetConnection();
        var result = await connection.QueryAsync<CategoryDTO>(SqlQueries.GetAllCategories);
        return result.ToList();
    }
}