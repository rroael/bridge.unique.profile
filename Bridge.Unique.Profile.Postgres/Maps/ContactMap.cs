using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Postgres.Entities;
using Bridge.Unique.Profile.System.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bridge.Unique.Profile.Postgres.Maps
{
    public class ContactMap : BaseAuditMap<ContactEntity, long>
    {
        public override void Configure(EntityTypeBuilder<ContactEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Contact");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .UseIdentityAlwaysColumn();

            builder.Property(f => f.Active)
                .HasDefaultValue(true);

            builder.Property(f => f.Email)
                .HasMaxLength(254)
                .IsRequired();

            builder.Property(f => f.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(f => f.Document)
                .HasMaxLength(32);

            builder.Property(f => f.ContactType)
                .HasDefaultValue((byte)EContactType.ADMINISTRATION)
                .IsRequired();

            builder.Property(f => f.PhoneNumber)
                .HasMaxLength(32)
                .IsRequired();

            builder.HasOne(f => f.Client)
                .WithMany(f => f.Contacts)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}