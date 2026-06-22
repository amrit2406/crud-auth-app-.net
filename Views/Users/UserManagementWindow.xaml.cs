using System.Windows;
using auth_app.Services;
using auth_app.Models;
using Microsoft.Extensions.DependencyInjection;

namespace auth_app.Views.Users;

public partial class UserManagementWindow : Window
{
    private readonly UserService _userService;

    public UserManagementWindow(
        UserService userService)
    {
        InitializeComponent();

        _userService = userService;

        Loaded += UserManagementWindow_Loaded;
    }

    private async void UserManagementWindow_Loaded(
        object sender,
        RoutedEventArgs e)
    {
        UsersGrid.ItemsSource =
            await _userService.GetAllAsync();
    }

    private async void AddButton_Click(
    object sender,
    RoutedEventArgs e)
    {
        var window =
            App.ServiceProvider
                .GetRequiredService<UserCreateWindow>();

        bool? result =
            window.ShowDialog();

        if (result == true)
        {
            UsersGrid.ItemsSource =
                await _userService.GetAllAsync();
        }
    }

    private async void EditButton_Click(
    object sender,
    RoutedEventArgs e)
    {
        if (((FrameworkElement)sender).DataContext
            is not User user)
            return;

        var window =
            new UserEditWindow(
                _userService,
                user);

        bool? result =
            window.ShowDialog();

        if (result == true)
        {
            UsersGrid.ItemsSource =
                await _userService.GetAllAsync();
        }
    }

    private async void DeleteButton_Click(
    object sender,
    RoutedEventArgs e)
    {
        if (((FrameworkElement)sender).DataContext
            is not User user)
            return;

        var confirm =
            MessageBox.Show(
                $"Delete {user.Username} ?",
                "Confirm",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

        if (confirm != MessageBoxResult.Yes)
            return;

        await _userService.DeleteAsync(user.Id);

        UsersGrid.ItemsSource =
            await _userService.GetAllAsync();
    }
}