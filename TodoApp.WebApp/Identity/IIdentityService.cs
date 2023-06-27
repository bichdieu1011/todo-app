namespace TodoApp.WebApp.Identity
{
    public interface IIdentityService
    {
        int UserId { get; }

        Task<int> GetUserId();
    }
}