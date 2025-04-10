using System.Text.Json;
using AralytiksTest2.Contracts;
using AralytiksTest2.DTO;
using AralytiksTest2.Models;

namespace AralytiksTest2.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
       
        public async Task<(IEnumerable<User>? users, int totalCount)> GetAllUsersAsync(int page, int pageSize)
        {
            try
            {
                (IEnumerable<User>? Entities, int TotalCount) allUsers = await _userRepository.GetAllWithPagingationAsync(page, pageSize);
                return (allUsers.Entities, allUsers.TotalCount);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong!");
                throw;
            }
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            try
            {
                User? user = await _userRepository.GetAsync(id);
                return user;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong!");
                throw;
            }
        }

        public async Task<User> CreateUserAsync(UserDto user)
        {
            try
            {
                User newUser = new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Password = user.Password,
                    Birthdate = user.Birthdate,
                    Settings = user.Settings != null ? JsonSerializer.Serialize(user.Settings) : string.Empty,
                    CreatedAt = DateTime.UtcNow
                };

                User createdUser = await _userRepository.AddAsync(newUser);
                await _unitOfWork.SaveChangesAsync();

                return createdUser;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong!");
                throw;
            }
        }

        public async Task<User?> UpdateUserAsync(int id, UserDto user)
        {
            try
            {
                User? findUser = await _userRepository.GetAsync(id);
                if (findUser == null)
                    return null;

                findUser.FirstName = user.FirstName;
                findUser.LastName = user.LastName;
                findUser.Username = user.Username;
                findUser.Password = user.Password;
                findUser.Birthdate = user.Birthdate;
                findUser.Settings = user.Settings != null ? JsonSerializer.Serialize(user.Settings) : string.Empty;
                findUser.UpdatedAt = DateTime.UtcNow;

                _userRepository.Update(findUser);
                await _unitOfWork.SaveChangesAsync();

                return findUser;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong!");
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                User? findUser = await _userRepository.GetAsync(id);
                if (findUser == null)
                    return false;

                _userRepository.Remove(findUser);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Something went wrong!");
                throw;
            }
        }
    }
}
