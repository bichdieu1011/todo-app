using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Database.Entities;

namespace TodoApp.Database.EntityConfigurations
{
    public class ActionItemConfigurations : IEntityTypeConfiguration<ActionItem>
    {
        public void Configure(EntityTypeBuilder<ActionItem> builder)
        {
            builder.ToTable(nameof(ActionItem));
            builder.HasKey(x => x.Id);
            builder.Property(s => s.CategoryId).IsRequired();
            builder.Property(s => s.Start).IsRequired();
            builder.Property(s => s.End).IsRequired();
            builder.Property(s => s.Status).IsRequired();
            builder.Property(s => s.Content).IsRequired().HasMaxLength(500);

            builder.HasOne(s => s.Category).WithMany().HasForeignKey(s => s.CategoryId);
            builder.HasOne(s => s.User).WithMany().HasForeignKey(s => s.UserId);

        }
    }
}