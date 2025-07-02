namespace UserApi.Services;
using UserApi.Entities;
using UserApi.Repositories;

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
}
