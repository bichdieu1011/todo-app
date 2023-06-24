using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Database.Entities;

namespace TodoApp.Database.EntityConfigurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));
            builder.HasKey(x => x.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(s => s.ActionItems)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId)
                .IsRequired();
        }
    }
}