using Client.Containers.State;
using Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

namespace Client.Pages.Set.ViewModels;

public class SetViewModel : BaseViewModel, ISetViewModel
{
    private int id;
    private string symbol = string.Empty;
    private string name = string.Empty;
    private string title = "Create";
    private bool isLoading = false;
    private readonly IJSRuntime jsRuntime;
    private readonly ISetService categoryService;
    private readonly NavigationManager navigationManager;

    public SetViewModel(ISetService categoryService, NavigationManager navigationManager, IJSRuntime jSRuntime)
    {
        this.categoryService = categoryService;
        this.navigationManager = navigationManager;
        jsRuntime = jSRuntime;
    }

    public int Id
    {
        get => id;
        set => id = value;
    }

    [Required]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "The {0} value cannot exceed {1} characters or is less than {2}")]
    public string Name
    {
        get => name;
        set => SetValue(ref name, value);
    }

    public string Symbol
    {
        get => symbol;
        private set => SetValue(ref symbol, value);
    }

    public string Title
    {
        get => title;
        set => SetValue(ref title, value);
    }

    public bool IsLoading
    {
        get => isLoading;
        set => SetValue(ref isLoading, value);
    }

    public async Task LoadCategory()
    {
        IsLoading = true;
        Name = string.Empty;
        Title = "Create";
        Symbol = string.Empty;
        if (Id == 0)
        {
            IsLoading = false;
        }
        else
        {
            var category = await categoryService.GetById(Id);
            Id = category.Id;
            Name = category.Name;
            Symbol = category.Symbol;
            Title = "Update";
            IsLoading = false;
        }
    }

    public async Task HandleImageUpload(InputFileChangeEventArgs e)
    {
        try
        {
            Symbol = await ImageHelper.GetImageDataAsync(e);
        }
        catch (Exception exception)
        {
            await jsRuntime.ToastrError(exception.Message);
        }
    }

    public async Task UpsertCategory()
    {
        IsLoading = true;
        if (Id == 0)
        {
            await categoryService.Create(new CategoryDTO { Name = Name, Symbol = Symbol });            
        }
        else
        {
            await categoryService.Update(new CategoryDTO { Id = Id, Name = Name, Symbol = Symbol });            
        }
        navigationManager.NavigateTo("set");
        await jsRuntime.ToastrSuccess(string.Empty);
        IsLoading = false;
    }
}
