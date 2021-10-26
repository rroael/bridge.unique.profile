using Bridge.Unique.Profile.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bridge.Unique.Profile.Postgres.Maps
{
    public class UserAddressMap : IEntityTypeConfiguration<UserAddressEntity>
    {
        public void Configure(EntityTypeBuilder<UserAddressEntity> builder)
        {
            builder.ToTable("UserAddress");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .UseIdentityAlwaysColumn();

            builder.HasIndex(f => new { f.UserId, f.AddressId }).IsUnique();

            builder.HasOne(f => f.User)
                .WithMany(ua => ua.Addresses)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(f => f.Address)
                .WithMany(ua => ua.UserAddresses)
                .HasForeignKey(a => a.AddressId);
        }
    }
}