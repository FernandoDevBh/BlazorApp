using Client.Containers.State;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace Client.Pages.Category.ViewModels
{
    public interface ICategoryListViewModel : IViewModel
    {
        IQueryable<CategoryDTO> Categories { get; }
        bool IsLoading { get; }
        bool ShowDeleteModal { get; }
        PaginationState Pagination { get; }
        Task LoadCategories();
        void SetIsLoading();
        void Delete(int id);
        Task DeleteConfirmation(bool isConfirmed);
    }
}