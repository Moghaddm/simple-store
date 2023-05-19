using CRUD.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.Configuration;
public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Name).HasMaxLength(50);

        builder.Property(prop => prop.Rate).HasDefaultValue(5);

        builder.HasMany(prop => prop.Comments).WithOne(p => p.Product).HasForeignKey("ProductId");

        builder.Property(prop => prop.Name).IsRequired();

        builder.Property(prop => prop.Description).IsRequired(false);

        builder.OwnsMany(prop => prop.Attachments , options => 
        {
            options.ToJson("Attachments");
        });
    }
}