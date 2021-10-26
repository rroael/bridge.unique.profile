using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bridge.Unique.Profile.Postgres.Maps
{
    public class AddressMap : BaseAuditMap<AddressEntity, long>
    {
        public override void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Address");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .UseIdentityAlwaysColumn();

            builder.Property(f => f.Active)
                .HasDefaultValue(true);

            builder.Property(f => f.City)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(f => f.Complement)
                .HasMaxLength(32);

            builder.Property(f => f.Country)
                .HasMaxLength(48)
                .IsRequired();

            builder.Property(f => f.Neighborhood)
                .HasMaxLength(48)
                .IsRequired();

            builder.Property(f => f.Nickname)
                .HasMaxLength(48)
                .IsRequired();

            builder.Property(f => f.State)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(f => f.Street)
                .HasMaxLength(254)
                .IsRequired();

            builder.Property(f => f.AddressTypes)
                .IsRequired();

            builder.Property(f => f.StreetNumber)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(f => f.ZipCode)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(f => f.Email)
                .HasMaxLength(254);

            builder.Property(f => f.Name)
                .HasMaxLength(128);

            builder.Property(f => f.PhoneNumber)
                .HasMaxLength(32);

            builder.Property(f => f.Latitude);
            builder.Property(f => f.Longitude);

            builder.HasMany(f => f.UserAddresses)
                .WithOne(f => f.Address)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}