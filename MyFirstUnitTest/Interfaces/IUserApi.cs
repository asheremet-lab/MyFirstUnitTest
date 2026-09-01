using Refit;
using MyFirstUnitTest.DTO;

namespace MyFirstUnitTest.Interfaces;

public interface IUserApi
{
    [Get("/api/users")]
    Task<UsersDataResponseDTO> GetUsersAsync();

    [Post("/api/users")]
    Task<UserResponseDTO> CreateUserAsync([Body] MyFirstUnitTest.DTO.CreateUserRequestDTO user);
}