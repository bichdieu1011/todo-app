using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TodoApp.Database
{
    public class ToDoAppContext : DbContext
    {
        public ToDoAppContext(DbContextOptions<ToDoAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoAppContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}