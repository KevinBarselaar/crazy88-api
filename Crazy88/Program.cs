using Crazy88Test.Business;
using Crazy88Test.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Crazy88Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

            ProcessDbCommands(host);

            host.Run();
        }

        private static void ProcessDbCommands(IWebHost host)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ApiContext>().Database.Migrate();

                scope.VerifyInitialDbSeed();
            }
        }

    }
}
