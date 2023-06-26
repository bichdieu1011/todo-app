using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TodoApp.WebApp.Identity
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;
        const string _emailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<string> GetUserIdentityEmail()
        {
            if (_context.HttpContext != null && 
                _context.HttpContext.User != null && 
                _context.HttpContext.User.Identity != null && 
                _context.HttpContext.User.Identity.IsAuthenticated)
            {
                return _context.HttpContext.User.Claims?.FirstOrDefault(s => s.Type == _emailClaim)?.Value;
            }

            return string.Empty;
        }

        public string GetUserName()
        {
            return _context.HttpContext.User.Identity.Name;
        }
    }
}