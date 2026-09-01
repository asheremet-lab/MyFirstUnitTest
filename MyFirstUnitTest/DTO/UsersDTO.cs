using System.Text.Json.Serialization;

namespace MyFirstUnitTest.DTO;

public record UsersDataResponseDTO(
    [property: JsonPropertyName("data")] List<UserDTO> Data
);

public record UserDTO(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("profile")] UserProfileDTO Profile,
    [property: JsonPropertyName("roles")] List<string> Roles
);

public record UserProfileDTO(
    [property: JsonPropertyName("fullName")] string FullName,
    [property: JsonPropertyName("age")] int Age,
    [property: JsonPropertyName("address")] UserAddressDTO Address,
    [property: JsonPropertyName("tags")] List<string> Tags
);

public record UserAddressDTO(
    [property: JsonPropertyName("street")] string Street,
    [property: JsonPropertyName("city")] string City,
    [property: JsonPropertyName("geo")] GeoCoordinatesDTO Geo
);

public record GeoCoordinatesDTO(
    [property: JsonPropertyName("lat")] double Lat,
    [property: JsonPropertyName("lng")] double Lng
);