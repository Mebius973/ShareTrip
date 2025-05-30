using Microsoft.AspNetCore.Mvc;
using UserApi.Entities;

namespace UserApi.Repositories
{
    public interface IUserRepository
    {
        Task<IResult> CreateAsync(UserEntity model);
        Task<LoggedInUserEntity?> LoginAsync(UserEntity model);
    }
}
