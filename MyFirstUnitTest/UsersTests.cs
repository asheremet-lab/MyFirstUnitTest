using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MyFirstUnitTest.DTO;
using MyFirstUnitTest.Utils;
using Xunit;

namespace MyFirstUnitTest;

public class UsersTests
{
    private readonly List<UserDTO> _users;

    public UsersTests()
    {
        var response = JsonFileReader.ReadAndDeserialize<UsersDataResponseDTO>(Path.Combine("Resources", "UsersData.json"));
        _users = response.Data;
    }

    [Fact]
    public void Test2_1_UsersCountShouldBeTen()
    {
        _users.Should().HaveCount(10);
    }

    [Fact]
    public void Test2_2_FirstUserShouldBeAliceJohnson()
    {
        var firstUser = _users.FirstOrDefault();
        firstUser.Should().NotBeNull();
        firstUser!.Profile.FullName.Should().Be("Alice Johnson");
    }

    [Fact]
    public void Test2_3_AllIdsShouldBeUnique()
    {
        var userIds = _users.Select(u => u.Id).ToList();
        userIds.Should().OnlyHaveUniqueItems();
    }

    [Fact]
    public void Test2_4_ShouldHaveAtLeastOnePremiumUserTag()
    {
        var hasPremiumUser = _users.Any(u => u.Profile.Tags != null && u.Profile.Tags.Contains("premium"));
        hasPremiumUser.Should().BeTrue();
    }

    [Fact]
    public void Test2_5_AllUsersShouldHaveNonEmptyCity()
    {
        _users.Select(u => u.Profile.Address.City)
              .Should()
              .AllSatisfy(city => city.Should().NotBeNullOrWhiteSpace());
    }

    [Fact]
    public void Test2_6_ShouldHaveAtLeastOneUserFromStockholm()
    {
        var hasStockholmUser = _users.Any(u => u.Profile.Address.City.Equals("Stockholm", StringComparison.OrdinalIgnoreCase));
        hasStockholmUser.Should().BeTrue();
    }

    [Fact]
    public void Test2_7_AllUsersAgeShouldBeBetween18And60()
    {
        _users.Select(u => u.Profile.Age)
              .Should()
              .AllSatisfy(age => age.Should().BeInRange(18, 60));
    }

    [Fact]
    public void Test2_8_ShouldHaveAtLeastOneUserWithAdminRole()
    {
        var hasAdminRole = _users.Any(u => u.Roles != null && u.Roles.Contains("admin", StringComparer.OrdinalIgnoreCase));
        hasAdminRole.Should().BeTrue();
    }

    [Fact]
    public void Test3_AllUsersCoordinatesShouldBeWithinSweden()
    {
        _users.Select(u => u.Profile.Address.Geo)
              .Should()
              .AllSatisfy(geo =>
              {
                  geo.Lat.Should().BeInRange(55.3, 69.1);
                  geo.Lng.Should().BeInRange(10.9, 24.2);
              });
    }

    [Fact]
    public void Test4_StreetsShouldMatchConditions()
    {
        _users.Select(u => u.Profile.Address.Street)
              .Should()
              .AllSatisfy(street =>
              {
                  street.Should().MatchRegex(@"\d");
                  char.IsLetter(street[0]).Should().BeTrue();
                  street.All(char.IsDigit).Should().BeFalse();
              });
    }
}