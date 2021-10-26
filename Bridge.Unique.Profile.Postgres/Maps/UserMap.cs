using Bridge.Commons.System.EntityFramework.Bases.Audits;
using Bridge.Unique.Profile.Postgres.Entities;
using Bridge.Unique.Profile.System.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bridge.Unique.Profile.Postgres.Maps
{
    public class UserMap : BaseAuditMap<UserEntity, int>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("User");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .UseIdentityAlwaysColumn();

            builder.Property(f => f.Active)
                .HasDefaultValue(true);

            builder.Property(f => f.Document)
                .HasMaxLength(32);

            builder.HasIndex(f => f.Document);

            builder.Property(f => f.Email)
                .HasMaxLength(254)
                .IsRequired();

            builder.HasIndex(f => f.Email)
                .IsUnique();

            builder.Property(f => f.Gender)
                .HasDefaultValue((byte)EGender.UNDEFINED)
                .IsRequired();

            builder.Property(f => f.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(f => f.Password)
                .HasMaxLength(128);

            builder.Property(f => f.BirthDate)
                .HasColumnType("DATE");

            builder.Property(f => f.ImageUrl)
                .HasMaxLength(1024);

            builder.Property(f => f.PhoneNumber)
                .HasMaxLength(32);

            builder.HasIndex(f => f.PhoneNumber);

            builder.Property(f => f.UserName)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(f => f.IsEmailValidated)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(f => f.IsPhoneNumberValidated)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(f => f.EmailValidationToken)
                .HasMaxLength(128);

            builder.Property(f => f.SmsValidationCode)
                .HasMaxLength(128);

            builder.HasIndex(f => f.UserName)
                .IsUnique();

            builder.HasMany(f => f.Addresses)
                .WithOne(f => f.User)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}