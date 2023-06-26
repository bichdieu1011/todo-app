using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using TodoApp.Database;
using TodoApp.Database.Entities;

namespace TodoApp.Services.UserService.Service
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;
        private readonly IMemoryCache memoryCache;
        private readonly ToDoAppContext appContext;

        public UserService(ILogger<UserService> logger, IMemoryCache memoryCache, ToDoAppContext appContext)
        {
            this.logger = logger;
            this.memoryCache = memoryCache;
            this.appContext = appContext;
        }

        public async Task<User> AddUser(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.Email)) throw new ArgumentNullException(nameof(user.Email));

            var checkExists = await appContext.Set<User>().FirstOrDefaultAsync(s => s.Email == user.Email);
            if (checkExists != null)
                return checkExists;

            var newUser = new User
            {
                Email = user.Email,
                JoinedDate = DateTime.Now
            };
            await appContext.Set<User>().AddAsync(newUser);
            await appContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<int> GetOrAddUser(string email)
        {
            var cacheKey = $"_useremail:{email.ToLower()}";
            if (!memoryCache.TryGetValue(cacheKey, out int userId))
            {
                var user = await appContext.Set<User>().FirstOrDefaultAsync(s => s.Email == email);
                if (user == null)
                {
                    user = await AddUser(new User { Email = email });
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));
                userId = user.Id;
                memoryCache.Set(cacheKey, userId, cacheEntryOptions);
            }

            return userId;
        }

        public async Task<int> GetUserIdByEmail(string email)
        {
            var cacheKey = $"_useremail:{email.ToLower()}";
            if (!memoryCache.TryGetValue(cacheKey, out int userId))
            {
                var user = await appContext.Set<User>().FirstOrDefaultAsync(s => s.Email == email);
                if (user == null)
                    return 0;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));
                userId = user.Id;
                memoryCache.Set(cacheKey, userId, cacheEntryOptions);
            }

            return userId;
        }
    }
}