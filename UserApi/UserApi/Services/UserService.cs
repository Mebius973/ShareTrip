using System.Collections.Immutable;

namespace UserApi.Services;
using UserApi.Entities;
using UserApi.Repositories;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;

public class UserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IResult> CreateAsync(CreateUserEntity model)
    {
        return await _userRepository.CreateAsync(model);
    }

    public async Task<ClaimsPrincipal?> LoginAsync(string email, string password, ImmutableArray<string> scopes)
    {
        (var user, var roles) = await _userRepository.LoginAsync(email, password);
        if (user == null)
        {
            return null;
        }

        var identity = new ClaimsIdentity(
            TokenValidationParameters.DefaultAuthenticationType,
            OpenIddictConstants.Claims.Name,
            OpenIddictConstants.Claims.Role);

        identity.AddClaim(OpenIddictConstants.Claims.Subject, user.Id);
        identity.AddClaim(OpenIddictConstants.Claims.Email, user.Email);
        identity.AddClaim(OpenIddictConstants.Claims.Name, user.UserName);

        // Ajoute les rôles si besoin
        foreach (var role in roles)
        {
            identity.AddClaim(OpenIddictConstants.Claims.Role, role);
        }

        var principal = new ClaimsPrincipal(identity);

        principal.SetScopes(scopes);
        principal.SetResources("resource_server"); // optionnel
        
        return principal;
    }
}
