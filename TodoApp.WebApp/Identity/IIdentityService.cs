namespace TodoApp.WebApp.Identity
{
    public interface IIdentityService
    {
        Task<string> GetUserIdentityEmail();

        string GetUserName();
    }
}
