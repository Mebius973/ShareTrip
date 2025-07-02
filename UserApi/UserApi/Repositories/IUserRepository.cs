namespace UserApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using UserApi.Entities;

public interface IUserRepository
{
    Task<IResult> CreateAsync(UserEntity model);
}
