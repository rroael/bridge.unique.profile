using Bridge.Unique.Profile.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bridge.Unique.Profile.Postgres.Maps
{
    public class UserLoginMap : IEntityTypeConfiguration<UserLoginEntity>
    {
        public void Configure(EntityTypeBuilder<UserLoginEntity> builder)
        {
            builder.ToTable("UserLogin");

            builder.HasKey(f => new { f.Provider, f.ProviderUserId, f.UserId });

            //builder.HasIndex(f => new {f.Provider, f.ProviderUserId}).IsUnique();
            builder.HasIndex(f => new { f.Provider, f.ProviderUserId, f.UserId }).IsUnique();

            builder.Property(f => f.Provider)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(f => f.ProviderUserId)
                .HasMaxLength(254)
                .ValueGeneratedNever()
                .IsRequired();

            builder.HasOne(f => f.User)
                .WithMany(ul => ul.UserLogins)
                .HasForeignKey(u => u.UserId);
        }
    }
}