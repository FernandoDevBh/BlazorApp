using Client.Containers.State;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace Client.Pages.Card.ViewModels;

public interface ICardListVM : IViewModel
{
    bool IsLoading { get; }
    bool ShowDeleteModal { get; }
    PaginationState Pagination { get; }
    void Delete(int id);        
    Task LoadCards();
    void SetIsLoading();
    Task DeleteConfirmation(bool isConfirmed);
    IQueryable<ProductDTO> Cards { get; }
}