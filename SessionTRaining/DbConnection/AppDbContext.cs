using Microsoft.EntityFrameworkCore;
using SessionTRaining.Models;

namespace SessionTRaining.DbConnection
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UsersToDoList> UsersToDoList { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> op) : base(op) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-H0K0LN7;Database=SessionTrainingDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
