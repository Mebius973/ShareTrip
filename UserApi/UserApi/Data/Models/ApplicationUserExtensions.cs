namespace UserApi.Data.Models;
using UserApi.Entities;

public static class ApplicationUserExtensions
{
    public static LoggedInUserEntity AsEntity(this ApplicationUser user)
    {
        return new LoggedInUserEntity
        {
            Id = user.Id,
            Email = user.Email!,
            UserName = user.Email!
        };
    }
}