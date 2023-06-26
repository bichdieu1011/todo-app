using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TodoApp.WebApp.Identity
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<string> GetUserIdentityEmail()
        {
            if (_context.HttpContext.User.Identity.IsAuthenticated)
            {
                var authenticate = await _context.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                return authenticate.Principal?.Claims.FirstOrDefault(s => s.Type == "Ultil.ClaimEmail")?.Value;
            }
            return string.Empty;
        }

        public string GetUserName()
        {
            return _context.HttpContext.User.Identity.Name;
        }
    }
}