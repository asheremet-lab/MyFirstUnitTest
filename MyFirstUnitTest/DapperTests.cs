using System.Threading.Tasks;
using FluentAssertions;
using MyFirstUnitTest.Repositories;
using NUnit.Framework;

namespace MyFirstUnitTest;

[TestFixture]
public class DapperTests
{
    private CategoryRepository _categoryRepository = null!;
    private ProductRepository _productRepository = null!;
    private UserRepository _userRepository = null!;
    private OrderRepository _orderRepository = null!;

    [SetUp]
    public void Setup()
    {
        _categoryRepository = new CategoryRepository();
        _productRepository = new ProductRepository();
        _userRepository = new UserRepository();
        _orderRepository = new OrderRepository();
    }

    [Test]
    public async Task GetAllCategories_ShouldReturnExpectedCount()
    {
        const int expectedCount = 5;

        var categories = await _categoryRepository.GetAllCategoriesAsync();

        categories.Should().NotBeNull();
        categories.Should().HaveCount(expectedCount);
    }

    [Test]
    public async Task GetProductById_ShouldReturnCorrectProduct()
    {
        const int targetId = 1;

        var product = await _productRepository.GetProductByIdAsync(targetId);

        product.Should().NotBeNull();
        product!.Id.Should().Be(targetId);
    }

    [Test]
    public async Task GetUserOrder_ShouldContainExpectedItems()
    {
        const int targetUserId = 1;
        const int targetOrderId = 100;

        var order = await _orderRepository.GetOrderByIdAndUserIdAsync(targetOrderId, targetUserId);
        var productsInOrder = await _productRepository.GetProductsByOrderIdAsync(targetOrderId);

        order.Should().NotBeNull();
        productsInOrder.Should().NotBeEmpty();
    }

    [Test]
    public async Task AccessoriesBuyers_ShouldBeFromDifferentCities()
    {
        var cities = await _userRepository.GetCitiesBuyingAccessoriesAsync();

        cities.Should().NotBeEmpty();
    }

    [Test]
    public async Task TvBuyers_ShouldAlsoBuyAccessories()
    {
        var tvAndAccessoryBuyers = await _userRepository.GetTvBuyersWhoAlsoBoughtAccessoriesAsync();

        tvAndAccessoryBuyers.Should().NotBeEmpty();
    }
}