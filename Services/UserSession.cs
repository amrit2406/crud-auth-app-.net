using auth_app.Models;

namespace auth_app.Services;

public static class UserSession
{
    public static User? CurrentUser { get; set; }

    public static List<string> Permissions { get; set; }
        = new();

    public static bool IsLoggedIn =>
        CurrentUser != null;

    public static void Logout()
    {
        CurrentUser = null;
        Permissions.Clear();
    }
}