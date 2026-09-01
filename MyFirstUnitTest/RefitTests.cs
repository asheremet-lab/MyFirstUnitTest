using FluentAssertions;
using MyFirstUnitTest.DTO;
using MyFirstUnitTest.Interfaces;
using Refit;
using Xunit;

namespace MyFirstUnitTest;

public class RefitTests
{
    private readonly IUserApi _userApi;

    public RefitTests()
    {
        _userApi = RestService.For<IUserApi>("https://reqres.in");
    }

    [Fact]
    public async Task GetUsersAsync_ShouldReturnUsersList()
    {
        var response = await _userApi.GetUsersAsync();

        response.Should().NotBeNull();
        response.Data.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnCreatedUser()
    {
        var request = new CreateUserRequestDTO("morpheus", "leader");

        var response = await _userApi.CreateUserAsync(request);

        response.Should().NotBeNull();
        response.Name.Should().Be("morpheus");
        response.Job.Should().Be("leader");
    }
}