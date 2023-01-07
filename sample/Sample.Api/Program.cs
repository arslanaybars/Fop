using Microsoft.AspNetCore;
using Sample.Data;

namespace Sample.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateWebHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                DbInitializer.SeedAsync(context).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        host.Run();

    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
}