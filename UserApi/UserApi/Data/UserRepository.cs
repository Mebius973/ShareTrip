using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data.Models;
using UserApi.Entities;
using UserApi.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
namespace UserApi.Data
{
    public class UserRepository: IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IResult> CreateAsync(UserEntity model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return TypedResults.BadRequest(result.Errors);

            return TypedResults.Ok();
        }
        public async Task<LoggedInUserEntity?> LoginAsync(UserEntity model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password)) {
                return new LoggedInUserEntity
                {
                    Id = user.Id,
                    Email = user.Email
                };
            }
            return null;
        }
    }
}
