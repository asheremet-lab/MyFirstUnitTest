using System.Text.Json.Serialization;

namespace MyFirstUnitTest.DTO;

public class UserResponseDTO
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("job")]
    public string? Job { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("createdAt")]
    public string? CreatedAt { get; set; }

    [JsonPropertyName("data")]
    public UserDataDTO? Data { get; set; }
}

public class UserDataDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("first_name")]
    public string? First_Name { get; set; }

    [JsonPropertyName("last_name")]
    public string? Last_Name { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }
}