using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crazy88Test.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Crazy88Test.Business
{
    public static class DataSeeder
    {
        public static void VerifyInitialDbSeed(this IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApiContext>();
            var hostingEnv = scope.ServiceProvider.GetRequiredService<IHostingEnvironment>();

            VerifyUsers(dbContext, hostingEnv);
        }

        private static void VerifyUsers(ApiContext context, IHostingEnvironment hostingEnvironment)
        {
            
        }
    }

}
