using auth_app.Helpers;
using auth_app.Services;
using auth_app.Views.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using auth_app.Views.Products;

namespace auth_app.Views.Dashboard;

public partial class DashboardWindow : Window
{
    private readonly AuthorizationService _auth;

    public DashboardWindow(
        AuthorizationService auth)
    {
        InitializeComponent();

        _auth = auth;

        Loaded += DashboardWindow_Loaded;
    }

    private void DashboardWindow_Loaded(
    object sender,
    RoutedEventArgs e)
    {
        //MessageBox.Show(
        //    $"Permissions Count: {UserSession.Permissions.Count}");

        UserText.Text =
            $"Logged in as: {UserSession.CurrentUser?.Username}";

        UsersButton.Visibility =
            _auth.HasPermission(
                Permissions.UserView)
                ? Visibility.Visible
                : Visibility.Collapsed;

        ProductsButton.Visibility = Visibility.Visible;

        //MessageBox.Show(
        //$"ProductView = {_auth.HasPermission(Permissions.ProductView)}");
    }
    private void UsersButton_Click(
    object sender,
    RoutedEventArgs e)
    {
        var window =
            App.ServiceProvider
                .GetRequiredService<UserManagementWindow>();

        window.ShowDialog();
    }

    private void ProductManagementButton_Click(
     object sender,
     RoutedEventArgs e)
    {
        var window =
            App.ServiceProvider
                .GetRequiredService<ProductManagementWindow>();

        window.ShowDialog();
    }
}