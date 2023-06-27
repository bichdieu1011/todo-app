using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Database.Entities;

namespace TodoApp.Database.EntityConfigurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(x => x.Id);
            builder.Property(s => s.Email).HasMaxLength(200);
            builder.Property(s => s.IdentifierId).IsRequired().HasMaxLength(200);

            builder.HasIndex(x => x.Email);
            builder.HasIndex(x => x.IdentifierId).IsUnique();

            builder.HasMany(s => s.ActionItems)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Categories)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}