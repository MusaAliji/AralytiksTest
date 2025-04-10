using AralytiksTest2.DTO;
using AralytiksTest2.Models;

namespace AralytiksTest2.Contracts
{
    public interface IUserService
    {
        Task<(IEnumerable<User>? users, int totalCount)> GetAllUsersAsync(int page, int pageSize);
        Task<User?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(UserDto user);
        Task<User?> UpdateUserAsync(int id, UserDto user);
        Task<bool> DeleteUserAsync(int id);
    }
}
