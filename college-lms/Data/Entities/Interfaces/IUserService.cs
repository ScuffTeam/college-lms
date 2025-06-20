using college_lms.Data.DTOs.Users;

namespace college_lms.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(CreateUserDto userDto);
        Task<UserDto> UpdateUserAsync(int id, UpdateUserDto userDto);
        Task DeleteUserAsync(int id);
    }
}