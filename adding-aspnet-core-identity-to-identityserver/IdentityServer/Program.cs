using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();

            using (var scope = builder.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var persistedGrantDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>())
                {
                    persistedGrantDbContext.Database.Migrate();
                }
                
                using (var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
                {
                    configurationDbContext.Database.Migrate();
                }
            }

            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}