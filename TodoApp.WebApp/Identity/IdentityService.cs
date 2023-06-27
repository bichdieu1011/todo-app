using Microsoft.Extensions.Caching.Memory;
using TodoApp.Database.Entities;
using TodoApp.Services.Models;
using TodoApp.Services.UserService.Service;

namespace TodoApp.WebApp.Identity
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;
        private readonly IMemoryCache _cache;
        private readonly IUserService userService;
        private const string _emailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        private const string _objectIdentifierClaim = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        public IdentityService(IHttpContextAccessor context, IMemoryCache cache, IUserService userService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this._cache = cache;
            this.userService = userService;
        }

        public int UserId
        {
            get
            {
                var profile = GetUserId().Result;
                return profile != null ? profile : 0;
            }
        }

        public async Task<int> GetUserId()
        {
            if (_context.HttpContext != null &&
                _context.HttpContext.User != null &&
                _context.HttpContext.User.Identity != null &&
                _context.HttpContext.User.Identity.IsAuthenticated)
            {
                var email = _context.HttpContext.User.Claims?.FirstOrDefault(s => s.Type == _emailClaim)?.Value;
                var identifierObjectId = _context.HttpContext.User.Claims?.FirstOrDefault(s => s.Type == _objectIdentifierClaim)?.Value;

                if (_cache.TryGetValue($"_userObjectId:{identifierObjectId.ToLower()}", out UserProfile userProfile))
                {
                    return userProfile.Id;
                }

                return await userService.GetOrAddUser(new UserProfile
                {
                    IdentifierObjectId = identifierObjectId,
                    Email = email
                });
            }

            return -1;
        }
    }
}