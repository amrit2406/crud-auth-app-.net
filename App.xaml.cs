using auth_app.Data;
using auth_app.Services;
using auth_app.ViewModels;
using auth_app.Views;
using auth_app.Views.Dashboard;
using auth_app.Views.Products;
using auth_app.Views.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace auth_app;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider
    {
        get;
        private set;
    } = null!;

    protected override void OnStartup(
        StartupEventArgs e)
    {
        var services =
            new ServiceCollection();

        services.AddDbContext<AppDbContext>(
            options =>
                options.UseSqlServer(
                    @"Data Source=VVSPL1\SQLEXPRESS;
                      Database=authncrud;
                      Trusted_Connection=True;
                      TrustServerCertificate=True"));

        services.AddScoped<AuthService>();
        services.AddScoped<LoginViewModel>();
        services.AddScoped<LoginWindow>();
        services.AddScoped<AuthorizationService>();
        services.AddScoped<DashboardWindow>();
        services.AddScoped<UserService>();
        services.AddScoped<UserManagementWindow>();
        services.AddScoped<UserCreateWindow>();
        services.AddTransient<UserEditWindow>();
        services.AddTransient<ProductService>();
        services.AddTransient<ProductManagementWindow>();
        services.AddTransient<ProductCreateWindow>();

        ServiceProvider =
            services.BuildServiceProvider();

        using var scope =
            ServiceProvider.CreateScope();

        var db =
            scope.ServiceProvider
                .GetRequiredService<AppDbContext>();

        db.Database.Migrate();

        DbSeeder.Seed(db);

        var login =
            ServiceProvider
                .GetRequiredService<LoginWindow>();

        login.Show();
    }
}