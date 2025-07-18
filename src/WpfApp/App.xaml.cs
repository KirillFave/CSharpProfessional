using DataAccess;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfApp;

public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        UserRepository userRepository = 
            ServiceProvider.GetRequiredService<UserRepository>();

        MainWindow mainWindow = new(userRepository);
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>();

        services.AddScoped(typeof(UserRepository));

        services.AddScoped<MainWindow>();
    }
}
