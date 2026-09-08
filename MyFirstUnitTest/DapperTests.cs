using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using MyFirstUnitTest.DTO;
using Xunit;

namespace MyFirstUnitTest;

public class DapperTests
{
    private const string ConnectionString = "Data Source=testdatabase.db";

    private IDbConnection GetConnection() => new SqliteConnection(ConnectionString);

    [Fact]
    public async Task GetAllCategories_ShouldReturnExpectedCount()
    {
        using var connection = GetConnection();
        const int expectedCount = 5;

        var categories = (await connection.QueryAsync<CategoryDTO>("SELECT * FROM Categories")).ToList();

        categories.Should().NotBeNull();
        categories.Should().HaveCount(expectedCount);
    }

    [Fact]
    public async Task GetProductById_ShouldReturnCorrectProduct()
    {
        using var connection = GetConnection();
        const int targetId = 1;

        var product = await connection.QueryFirstOrDefaultAsync<ProductDTO>(
            "SELECT * FROM Products WHERE Id = @Id",
            new { Id = targetId });

        product.Should().NotBeNull();
        product!.Id.Should().Be(targetId);
    }

    [Fact]
    public async Task GetUserOrder_ShouldContainExpectedItems()
    {
        using var connection = GetConnection();
        const int targetUserId = 1;
        const int targetOrderId = 100;

        var order = await connection.QueryFirstOrDefaultAsync<OrderDTO>(
            "SELECT * FROM Orders WHERE Id = @OrderId AND UserId = @UserId",
            new { OrderId = targetOrderId, UserId = targetUserId });

        const string sql = @"
            SELECT p.* 
            FROM Products p
            INNER JOIN OrderItems oi ON p.Id = oi.ProductId
            WHERE oi.OrderId = @OrderId";

        var productsInOrder = (await connection.QueryAsync<ProductDTO>(sql, new { OrderId = targetOrderId })).ToList();

        order.Should().NotBeNull();
        productsInOrder.Should().NotBeEmpty();
    }
}