using auth_app.ViewModels;
using System.Windows;

namespace auth_app.Views;

public partial class LoginWindow : Window
{
    private readonly LoginViewModel _vm;

    public LoginWindow(LoginViewModel vm)
    {
        InitializeComponent();

        _vm = vm;

        DataContext = vm;
    }

    private void Login_Click(
        object sender,
        RoutedEventArgs e)
    {
        _vm.Password =
            PasswordBox.Password;

        _vm.LoginCommand.Execute(null);
    }
}