using CourseWorkZPG.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkZPG
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("jsconfig1.json", optional: false, reloadOnChange: true)
                .Build();

            // Get the connection string from the appsettings.json file
            string connectionString = configuration.GetConnectionString("Postgres");


            // Configure the DbContext to use the PostgreSQL provider and the connection string from appsettings.json
            services.AddDbContext<MyDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Add logging to display EF Core database commands
            services.AddLogging(builder => {
                builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Debug)
                       .AddConsole();
            });
        }
    }
}
