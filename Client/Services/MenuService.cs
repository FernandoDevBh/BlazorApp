using Client.Components.Navigation.Menu.Models;
using Client.Components.Navigation.Menu.Services;

namespace Client.Services;

public class MenuService : IMenuService
{
    public List<MenuItem> GetMenus()
    {
        return new List<MenuItem>
        {
            CreateMenuItem(title : "Categories", icon: "LayoutTextSidebarReverse", navigateTo: "category"),
            CreateMenuItem(title : "Dashboard", icon: "DashboardFill"),
            CreateMenuItem(title : "Pages", icon: "AiOutlineFileText"),
            CreateMenuItem(title : "Media", icon: "FileImageFill"),            
            CreateMenuItem(title: "Analytics", icon: "AiOutLineBarChart"),
            CreateMenuItem(title: "Inbox", icon: "AiOutlineMail"),
            CreateMenuItem(title: "Profile", icon: "BsPerson"),
            CreateMenuItem(title : "Settings", icon: "AiOutlineSetting"),
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
