using System.Windows;
using auth_app.Models;
using auth_app.Services;

namespace auth_app.Views.Users;

public partial class UserEditWindow : Window
{
    private readonly UserService _userService;

    private readonly User _user;

    public UserEditWindow(
        UserService userService,
        User user)
    {
        InitializeComponent();

        _userService = userService;
        _user = user;

        UsernameBox.Text = user.Username;
        ActiveCheckBox.IsChecked = user.IsActive;
    }

    private async void Save_Click(
        object sender,
        RoutedEventArgs e)
    {
        _user.Username = UsernameBox.Text;
        _user.IsActive =
            ActiveCheckBox.IsChecked ?? true;

        await _userService.UpdateAsync(_user);

        DialogResult = true;
    }
}