namespace UserApi.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using UserApi.Data.Models;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<OpenIddictEntityFrameworkCoreApplication> OpenIddictApplications { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreAuthorization> OpenIddictAuthorizations { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreScope> OpenIddictScopes { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreToken> OpenIddictTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseOpenIddict();
    }
}
    


