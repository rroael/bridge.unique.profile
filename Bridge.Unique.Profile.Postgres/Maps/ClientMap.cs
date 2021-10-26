using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bridge.Unique.Profile.Postgres.Maps
{
    public class ClientMap : BaseAuditMap<ClientEntity, int>
    {
        public override void Configure(EntityTypeBuilder<ClientEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Client");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .UseIdentityAlwaysColumn();

            builder.Property(f => f.Active)
                .HasDefaultValue(true);

            builder.Property(f => f.Description)
                .HasMaxLength(254)
                .IsRequired();

            builder.Property(f => f.Code)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(f => f.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(f => f.Document)
                .HasMaxLength(32);

            builder.Property(f => f.Segment)
                .HasMaxLength(128);

            builder.HasIndex(f => f.Code)
                .IsUnique();

            builder.HasMany(f => f.Contacts)
                .WithOne(f => f.Client)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}