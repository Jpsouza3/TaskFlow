using CliFx;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TaskFlow.Business.Interface;
using TaskFlow.Core.Commands;
using TaskFlow.Data;
using TaskFlow.Data.Interface;
using TaskFlow.Data.Repository;

public static class Program
{
    public static async Task<int> Main()
    {
        string connectionString = "Data Source=taskflow.db;Version=3;";

        var services = new ServiceCollection();

        services.AddSingleton<IConnectionFactory>(_ => new SQLiteConnectionFactory(connectionString));
        services.AddTransient<ITaskRepository, TaskRepository>();

        services.AddTransient<TaskCommands>();

        var serviceProvider = services.BuildServiceProvider();
        DatabaseInitializer.InitializeDatabase(serviceProvider.GetRequiredService<IConnectionFactory>());

        return await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .UseTypeActivator(serviceProvider)
            .Build()
            .RunAsync();
    }
}