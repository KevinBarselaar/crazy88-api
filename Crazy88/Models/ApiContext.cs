using Microsoft.EntityFrameworkCore;

namespace Crazy88Test.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Team { get; set; }
        public DbSet<Minigame> Minigame { get; set; }
        public DbSet<Session> Session { get; set; }
    }
}