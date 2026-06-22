using auth_app.Services;
using auth_app.ViewModels.Base;
using auth_app.Views.Dashboard;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;

namespace auth_app.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly AuthService _authService;

    private string _username = "";

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel(
        AuthService authService)
    {
        _authService = authService;

        LoginCommand =
            new RelayCommand(async _ =>
            {
                var user =
                    await _authService.LoginAsync(
                        Username,
                        Password);

                if (user == null)
                {
                    MessageBox.Show(
                        "Invalid credentials");

                    return;
                }

                var dashboard =
                    App.ServiceProvider
                       .GetRequiredService<DashboardWindow>();

                dashboard.Show();

                Application.Current.Windows[0]?.Close();
            });
    }

    public string Password { get; set; } = "";
}