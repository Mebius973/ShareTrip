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
            {
                var errors = result.Errors.Select(e => new { e.Code, e.Description });
                return TypedResults.BadRequest(errors);
            }

            return TypedResults.Ok(new { user.Id, user.Email });
        }
    }
}
