using Client.Containers.State;
using Client.Components.Navigation.Menu.Services;

namespace Client.Components.Navigation.Menu.ViewModel;

public class MenuViewModel : BaseViewModel, IMenuViewModel
{
    private bool isOpen = true;
    private string rotate = string.Empty;    
    private List<IMenuItemViewModel> items = new List<IMenuItemViewModel>();

    public MenuViewModel(IMenuService service)
    {
        items = service.GetMenus().Select(m => new MenuItemViewModel(m, this)).Cast<IMenuItemViewModel>().ToList();        
    }

    public string Rotate
    {
        get => rotate;
        private set
        {
            SetValue(ref rotate, value);
        }
    }

    public bool IsOpen
    {
        get { return isOpen; }
        private set
        {            
            SetValue(ref isOpen, value);
        }
    }

    public List<IMenuItemViewModel> Items
    {
        get { return items; }
        set
        {
            items = value;
            SetValue(ref items, value);
        }
    }

    public void SetOpen()
    {
        IsOpen = !IsOpen;
        var rotate = string.Empty;
        if (!IsOpen) rotate = "rotate-180";
        Rotate = rotate;
    }
}
