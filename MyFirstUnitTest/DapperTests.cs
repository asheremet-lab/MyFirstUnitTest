using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using MyFirstUnitTest.DTO;
using MyFirstUnitTest.Utils;
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

        var categories = (await connection.QueryAsync<CategoryDTO>(SqlQueries.GetAllCategories)).ToList();

        categories.Should().NotBeNull();
        categories.Should().HaveCount(expectedCount);
    }

    [Fact]
    public async Task GetProductById_ShouldReturnCorrectProduct()
    {
        using var connection = GetConnection();
        const int targetId = 1;

        var product = await connection.QueryFirstOrDefaultAsync<ProductDTO>(
            SqlQueries.GetProductById,
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
            SqlQueries.GetOrderByIdAndUserId,
            new { OrderId = targetOrderId, UserId = targetUserId });

        var productsInOrder = (await connection.QueryAsync<ProductDTO>(
            SqlQueries.GetProductsByOrderId,
            new { OrderId = targetOrderId })).ToList();

        order.Should().NotBeNull();
        productsInOrder.Should().NotBeEmpty();
    }

    [Fact]
    public async Task AccessoriesBuyers_ShouldBeFromDifferentCities()
    {
        using var connection = GetConnection();

        var cities = (await connection.QueryAsync<string>(SqlQueries.GetCitiesBuyingAccessories)).ToList();

        cities.Should().NotBeEmpty();
        cities.Select(c => c.ToLower()).Distinct().Count().Should().BeGreaterThan(1);
    }

    [Fact]
    public async Task TvBuyers_ShouldAlsoBuyAccessories()
    {
        using var connection = GetConnection();

        var tvAndAccessoryBuyers = (await connection.QueryAsync<int>(SqlQueries.GetTvBuyersWhoAlsoBoughtAccessories)).ToList();

        tvAndAccessoryBuyers.Should().NotBeEmpty();
    }
}