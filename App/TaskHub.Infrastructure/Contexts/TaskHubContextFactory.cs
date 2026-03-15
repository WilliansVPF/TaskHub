using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TaskHub.Infrastructure.Contexts;

public class TaskHubContextFactory : IDesignTimeDbContextFactory<TaskHubContext>
{
    public TaskHubContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(basePath, "../TaskHub.Api"))
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TaskHubContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString);

        return new TaskHubContext(optionsBuilder.Options);
    }
}