using Client.Containers.State;
using Client.Components.Navigation.Menu.Models;

namespace Client.Components.Navigation.Menu.ViewModel;

public class MenuItemViewModel : BaseViewModel, IMenuItemViewModel
{
    private MenuItem item;
    private bool haveSubMenu;
    private IMenuViewModel menuViewModel;

    private bool subMenuIsOpen;

    public MenuItemViewModel(MenuItem item, IMenuViewModel menuViewModel)
    {
        this.item = item;
        this.menuViewModel = menuViewModel;
        this.haveSubMenu = item.SubMenuItems.Any();
    }

    public bool MenuIsOpen { get => menuViewModel.IsOpen; }

    public bool SubMenuIsOpen
    {
        get => subMenuIsOpen;
        private set
        {
            SetValue(ref subMenuIsOpen, value);
        }
    }    

    public List<MenuItem> SubMenus { get => Item.SubMenuItems; }

    public bool HaveSubMenu { get => haveSubMenu; }

    public MenuItem Item
    {
        get => item;
        private set
        {
            SetValue(ref item, value);
        }
    }

    public void SetOpen()
    {
        SubMenuIsOpen = MenuIsOpen && !SubMenuIsOpen;
    }

}
