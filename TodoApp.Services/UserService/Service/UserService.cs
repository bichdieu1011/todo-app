﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using TodoApp.Database;
using TodoApp.Database.Entities;
using TodoApp.Services.Models;

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

        public async Task<User> AddUser(UserProfile user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.IdentifierObjectId)) throw new ArgumentNullException(nameof(user.IdentifierObjectId));

            var checkExists = await appContext.Set<User>().FirstOrDefaultAsync(s => s.IdentifierId == user.IdentifierObjectId);
            if (checkExists != null)
                return checkExists;

            var newUser = new User
            {
                Email = user.Email,
                IdentifierId = user.IdentifierObjectId,
                JoinedDate = DateTime.Now
            };
            await appContext.Set<User>().AddAsync(newUser);
            await appContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<int> GetOrAddUser(UserProfile user)
        {
            var cacheKey = $"_userObjectId:{user.IdentifierObjectId.ToLower()}";
            if (!memoryCache.TryGetValue(cacheKey, out int userId))
            {
                var userDetails = await appContext.Set<User>().FirstOrDefaultAsync(s => s.IdentifierId == user.IdentifierObjectId);
                if (userDetails == null)
                {
                    userDetails = await AddUser(user);
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));
                userId = userDetails.Id;
                memoryCache.Set(cacheKey, userId, cacheEntryOptions);
            }

            return userId;
        }

        public async Task<int> GetUserId(UserProfile userProfile)
        {
            var cacheKey = $"_userObjectId:{userProfile.IdentifierObjectId.ToLower()}";
            if (!memoryCache.TryGetValue(cacheKey, out int userId))
            {
                var user = await appContext.Set<User>()
                    .FirstOrDefaultAsync(s => s.IdentifierId == userProfile.IdentifierObjectId);
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