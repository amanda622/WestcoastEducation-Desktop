using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Client.Services;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using WestcoastEducation.Models;


namespace Client;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        //Console.WriteLine($"RestDb BaseUrl: {config["RestDb:BaseUrl"]}");

        var services = new ServiceCollection();
        services.AddSingleton(config);

        services.AddSingleton<IHttpService<Course>, HttpService<Course>>();
        services.AddSingleton<ICourseService, RestDbCourseService>();

        services.AddSingleton<IHttpService<Student>, HttpService<Student>>();
        services.AddSingleton<IStudentService, RestDbStudentService>();

        services.AddSingleton<IHttpService<Teacher>, HttpService<Teacher>>();
        services.AddSingleton<ITeacherService, RestDbTeacherService>();

        services.AddSingleton<MainWindowViewModel>();

        var provider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}