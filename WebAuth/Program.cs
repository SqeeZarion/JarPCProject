using System.Data.SqlClient;
using JarPControlProject.Database;
using WebApplication3;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var host = CreateHostBuilder(args, configuration).Build();

        try
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<DBConnection>();

                Console.WriteLine("Підключення до бази даних встановлено успішно.");
                // Ваш код для взаємодії з базою даних
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка при підключенні до бази даних: " + ex.Message);
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseConfiguration(configuration);
            });
}