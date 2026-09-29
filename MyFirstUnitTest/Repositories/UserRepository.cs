using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using MyFirstUnitTest.Utils;

namespace MyFirstUnitTest.Repositories;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString = "Data Source=testdatabase.db")
    {
        _connectionString = connectionString;
    }

    private IDbConnection GetConnection() => new SqliteConnection(_connectionString);

    public async Task<List<string>> GetCitiesBuyingAccessoriesAsync()
    {
        using var connection = GetConnection();
        var result = await connection.QueryAsync<string>(SqlQueries.GetCitiesBuyingAccessories);
        return result.ToList();
    }

    public async Task<List<int>> GetTvBuyersWhoAlsoBoughtAccessoriesAsync()
    {
        using var connection = GetConnection();
        var result = await connection.QueryAsync<int>(SqlQueries.GetTvBuyersWhoAlsoBoughtAccessories);
        return result.ToList();
    }
}