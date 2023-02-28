using Client.Components.Navigation.Menu.Models;
using Client.Components.Navigation.Menu.Services;

namespace Client.Services;

public class MenuService : IMenuService
{
    public List<MenuItem> GetMenus()
    {
        return new List<MenuItem>
        {            
            CreateMenuItem(title : "Logout", icon: "AiOutlineLogout", navigateTo: "logout"),
        };
    }

    private MenuItem CreateMenuItem(string title,
                                    string? icon = null,                                    
                                    List<MenuItem>? items = null,
                                    string? navigateTo = null) =>
        new()
        {
            Title = title,
            Icon = string.IsNullOrEmpty(icon) ? string.Empty : $"Client.Icons.{icon}, Client.Icons",
            SubMenuItems = items ?? new List<MenuItem>(),
            SubMenu = (items != null && items.Any()),
            NavigateTo = navigateTo
        };           
}
