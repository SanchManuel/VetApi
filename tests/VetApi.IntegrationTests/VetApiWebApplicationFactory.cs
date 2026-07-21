using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace VetApi.IntegrationTests
{
    public class VetApiWebApplicationFactory : WebApplicationFactory<Program>
    {

        protected override void ConfigureWebHost(
         IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.UseSetting(
                "ConnectionStrings:PostgreSQL",
                "Host=localhost;Port=5432;" +
                "Database=vetapi_test;" +
                "Username=vetapi;" +
                "Password=testing");
        }

    }
}