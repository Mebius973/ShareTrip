using UserApi.Entities;
using UserApi.Repositories;

namespace UserApi.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IResult> CreatAsync(UserEntity model)
        {
            return await _userRepository.CreateAsync(model);
        }

        public async Task<LoggedInUserEntity?> LoginAsync(UserEntity model)
        {
            return await _userRepository.LoginAsync(model);
        }
    }
}
