using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevChatter.Sample.Web.Tests
{
    public class GreetingApi : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.AddScoped(sp =>
                {
                    return new DbContextOptionsBuilder<GreetingDbContext>()
                                .UseInMemoryDatabase("TestGreetingDb", root)
                                .UseApplicationServiceProvider(sp)
                                .Options;
                });
            });

            return base.CreateHost(builder);
        }
    }
}