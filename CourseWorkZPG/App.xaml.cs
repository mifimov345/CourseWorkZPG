using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using CourseWorkZPG.DBModels;

namespace CourseWorkZPG
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Create a new instance of the Startup class
            var startup = new Startup();

            // Create a new service collection
            var services = new ServiceCollection();

            // Call the ConfigureServices method of the Startup class and pass the service collection
            startup.ConfigureServices(services);

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Get an instance of the DbContext from the service provider
            var dbContext = serviceProvider.GetRequiredService<MyDbContext>();

        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("jsconfig1.json", optional: false, reloadOnChange: true)
                .Build();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<MyDbContext>(options =>
                        options.UseNpgsql(configurationBuilder.GetConnectionString("Postgres")));
                })
                .Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<MyDbContext>();
                    if (dbContext.Database.GetDbConnection().State != System.Data.ConnectionState.Open)
                    {
                        await dbContext.Database.GetDbConnection().OpenAsync();
                    }

                    await dbContext.Database.EnsureCreatedAsync();
                    //await InitializingDb.Initialize(dbContext);
                }
                catch (Exception ex)
                {
                    // handle exception
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine($"PostgreSQL exception: {ex.Message}");
                }
            }
        }
    }
}
