namespace UserApi.Infrastructure.Seeders;

using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

public static class OpenIddictSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        List<string> clients = new[] {"postman-client", "shareTripFront-client"}.ToList();
        
        foreach ( var client in clients)
        {
            if (await manager.FindByClientIdAsync(client) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = client,
                    ClientSecret = "secret",
                    DisplayName = $"Client for testing via {client}",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile
                    }
                });
            }
        }
    }
}