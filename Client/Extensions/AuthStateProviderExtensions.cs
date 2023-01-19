using Client.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Extensions;

public static class AuthStateProviderExtensions
{
    public static void NotifyUserLoggedIn(this AuthenticationStateProvider provider, string token)
    {
        ((AuthStateProvider)provider).NotifyUserLoggedIn(token);
    }

    public static void NotifyUserLoggedOut(this AuthenticationStateProvider provider)
    {
        ((AuthStateProvider)provider).NotifyUserLoggedOut();
    }
}
