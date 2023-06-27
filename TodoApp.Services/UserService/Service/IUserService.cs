using TodoApp.Database.Entities;
using TodoApp.Services.Models;

namespace TodoApp.Services.UserService.Service
{
    public interface IUserService
    {
        Task<int> GetUserId(UserProfile user);

        Task<User> AddUser(UserProfile user);

        Task<int> GetOrAddUser(UserProfile user);
    }
}