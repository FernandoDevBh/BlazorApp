using Client.Containers.State;
using Client.Components.Navigation.Menu.Models;

namespace Client.Components.Navigation.Menu.ViewModel
{
    public interface IMenuItemViewModel : IViewModel
    {
        bool HaveSubMenu { get; }
        public bool MenuIsOpen { get; }
        bool SubMenuIsOpen { get; }
        MenuItem Item { get; }
        List<MenuItem> SubMenus { get; }

        void SetOpen();
    }
}