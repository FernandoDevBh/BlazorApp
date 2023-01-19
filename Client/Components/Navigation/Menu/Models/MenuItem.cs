using Microsoft.AspNetCore.Components;

namespace Client.Components.Navigation.Menu.Models;
public class MenuItem
{
    public MenuItem()
    {
        SubMenuItems = new List<MenuItem>();
    }

    public required string Title { get; set; }
    public bool Spacing { get; set; } = false;
    public bool SubMenu { get; set; } = false;
    public string? Icon { get; set; } = null!;
    public string? NavigateTo { get; set; } = "#";

    public List<MenuItem> SubMenuItems { get; set; }
}
