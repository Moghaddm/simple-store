using CRUD.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Services.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public class StoreContext : IdentityDbContext<ApplicationUser,ApplicationRoles,string>
{
    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    builder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
    builder.Entity<IdentityUser>(entity =>
    {
        entity.ToTable(name: "User");
    });
    builder.Entity<IdentityRole>(entity =>
    {
        entity.ToTable(name: "Role");
    });
    builder.Entity<IdentityUserRole<string>>(entity =>
    {
        entity.ToTable("UserRoles");
    });
    builder.Entity<IdentityUserClaim<string>>(entity =>
    {
        entity.ToTable("UserClaims");
    });
    builder.Entity<IdentityUserLogin<string>>(entity =>
    {
        entity.ToTable("UserLogins");
    });
    builder.Entity<IdentityRoleClaim<string>>(entity =>
    {
        entity.ToTable("RoleClaims");
    });
    builder.Entity<IdentityUserToken<string>>(entity =>
    {
        entity.ToTable("UserTokens");
    });
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("MSSQL")
        );
}
