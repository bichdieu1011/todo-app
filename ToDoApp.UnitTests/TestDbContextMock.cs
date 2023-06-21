using Microsoft.EntityFrameworkCore;
using TodoApp.Database;
using TodoApp.Database.Entities;

namespace ToDoApp.UnitTests
{
    public class TestDbContextMock : ToDoAppContext
    {
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<ActionItem> ActionItems { get; set; }

        public TestDbContextMock(DbContextOptions<ToDoAppContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }
        }
    }
}