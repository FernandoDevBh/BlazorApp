using Client.Components.Navigation.Menu.Models;

namespace Client.Components.Navigation.Menu.Services
{
    public interface IMenuService
    {
        List<MenuItem> GetMenus();
    }
}
