namespace UserApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using UserApi.Entities;

public interface IUserRepository
{
    Task<IResult> CreateAsync(CreateUserEntity model);
    Task<(LoggedInUserEntity?, IList<string>)> LoginAsync(String email, string password);
}
