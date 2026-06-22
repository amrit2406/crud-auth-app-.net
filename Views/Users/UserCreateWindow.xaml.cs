using System.Windows;
using auth_app.Helpers;
using auth_app.Models;
using auth_app.Services;

namespace auth_app.Views.Users;

public partial class UserCreateWindow : Window
{
    private readonly UserService _userService;

    public UserCreateWindow(
        UserService userService)
    {
        InitializeComponent();

        _userService = userService;
    }

    private async void Save_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(
            UsernameTextBox.Text))
        {
            MessageBox.Show(
                "Username is required");

            return;
        }

        if (string.IsNullOrWhiteSpace(
            PasswordBox.Password))
        {
            MessageBox.Show(
                "Password is required");

            return;
        }

        var user = new User
        {
            Username = UsernameTextBox.Text,
            PasswordHash =
                PasswordHelper.HashPassword(
                    PasswordBox.Password),

            IsActive =
                ActiveCheckBox.IsChecked ?? true
        };

        await _userService.AddAsync(user);

        DialogResult = true;

        Close();
    }
}