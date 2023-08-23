using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopWebAPI.Model;

namespace OnlineShopWebAPI.Database.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(32);
            builder.Property(x => x.Username).HasMaxLength(32);
            builder.Property(x => x.Lastname).HasMaxLength(32);
            builder.Property(x => x.AccountType).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(255);
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.AccountStatus).IsRequired();
            builder.Property(x => x.Address).IsRequired();
        }
    }
}
