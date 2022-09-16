using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using server.tests.Data;

public class FunctionalTestFixture : WebApplicationFactory<Program>
{
    InMemoryDbFixture InMemoryDb;

    public FunctionalTestFixture() {
        InMemoryDb = new InMemoryDbFixture();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => {
            services.AddScoped<AppDbOptionsBuilder>(sp => new AppDbOptionsBuilder(InMemoryDb.DbContextOptions));
        });
        
        base.ConfigureWebHost(builder);
    }

    protected override void Dispose(bool disposing)
    {
        InMemoryDb.Dispose();
        base.Dispose(disposing);
    }



}