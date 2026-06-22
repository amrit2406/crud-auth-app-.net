namespace auth_app.Services;

public class AuthorizationService
{
    public bool HasPermission(string permission)
    {
        return UserSession
            .Permissions
            .Contains(permission);
    }
}