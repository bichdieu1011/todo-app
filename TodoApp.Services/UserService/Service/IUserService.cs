using TodoApp.Database.Entities;

namespace TodoApp.Services.UserService.Service
{
    public interface IUserService
    {
        Task<int> GetUserIdByEmail(string email);

        Task<User> AddUser(User user);

        Task<int> GetOrAddUser(string email);

    }
}