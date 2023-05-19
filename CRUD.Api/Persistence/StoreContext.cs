using CRUD.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Services.Authentication;

namespace Persistence;

public class StoreContext /* : IdentityDbContext<ApplicationUser> */ : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; } 
    public DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder) =>
        builder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MSSQL"));
}
