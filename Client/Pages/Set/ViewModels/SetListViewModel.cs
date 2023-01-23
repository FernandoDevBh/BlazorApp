using Client.Containers.State;
using Client.Services.Contracts;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.JSInterop;

namespace Client.Pages.Set.ViewModels;

public class SetListViewModel : BaseViewModel, ISetListViewModel
{
    private bool isLoading = false;
    private int deleteCategoryId = 0;
    private bool showDeleteModal = false;
    private IQueryable<CategoryDTO> categories = new List<CategoryDTO>(0).AsQueryable();
    private PaginationState pagination = new PaginationState { ItemsPerPage = 8 };
    private readonly ISetService service;
    private readonly IJSRuntime jSRuntime;

    public SetListViewModel(ISetService service, IJSRuntime jSRuntime)
    {
        this.service = service;
        this.jSRuntime = jSRuntime;
    }

    public async Task LoadCategories()
    {
        IsLoading = true;
        Categories = (await service.GetAll()).AsQueryable();
        IsLoading = false;
    }

    public bool IsLoading
    {
        get => isLoading;
        private set => SetValue(ref isLoading, value);
    }

    public PaginationState Pagination
    {
        get => pagination;
        private set => SetValue(ref pagination, value);
    }

    public IQueryable<CategoryDTO> Categories
    {
        get => categories;
        private set => SetValue(ref categories, value);
    }

    public bool ShowDeleteModal
    {
        get => showDeleteModal;
        private set => SetValue(ref showDeleteModal, value);
    }

    public void SetIsLoading() => IsLoading = !IsLoading;

    public void Delete(int id)
    {
        deleteCategoryId = id;
        ShowDeleteModal = true;
    }

    public async Task DeleteConfirmation(bool isConfirmed)
    {
        IsLoading = true;
        if (isConfirmed && deleteCategoryId != 0)
        {
            await service.DeleteById(deleteCategoryId);
            await LoadCategories();
            await jSRuntime.ToastrSuccess("Collection deleted succesfully");
        }
        showDeleteModal = false;
        IsLoading = false;
    }
}
