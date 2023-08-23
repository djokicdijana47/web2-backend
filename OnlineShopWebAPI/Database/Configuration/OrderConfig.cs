using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopWebAPI.Model;

namespace OnlineShopWebAPI.Database.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CustomerEmail).IsRequired();
            builder.Property(x => x.CustomerAddress).IsRequired();
            builder.Property(x => x.OrderPlacedTime).IsRequired();
        }
    }
}
