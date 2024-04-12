using Microsoft.EntityFrameworkCore;

namespace Lab8_9.Model
{
    public class Context : DbContext
    {
        public Context() { }

        public DbSet<Animal> Animals { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
          => builder.UseSqlite(@"Data Source=Lab8.db");

    }
}
