using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Crazy88Test.Models
{
    public class ApiContextFactory : IDesignTimeDbContextFactory<ApiContext>
    {
        public ApiContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LeaderboardDB;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ApiContext(optionsBuilder.Options);
        }
    }
}