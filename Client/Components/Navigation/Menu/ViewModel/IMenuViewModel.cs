using Client.Containers.State;

namespace Client.Components.Navigation.Menu.ViewModel
{
    public interface IMenuViewModel : IViewModel
    {
        bool IsOpen { get; }
        string Rotate { get; }
        List<IMenuItemViewModel> Items { get; set; }
        void SetOpen();
    }
}