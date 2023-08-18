using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Mapping;

namespace ToDoList.Infrastructure.Context
{
    public class DatabaseContext : DbContext
    {
       public DatabaseContext()
       {
       }

       public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
       {
       }

       public DbSet<ToDoItem> ToDoItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=./database.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ToDoItem>(new ToDoItemMapping().Configure);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}