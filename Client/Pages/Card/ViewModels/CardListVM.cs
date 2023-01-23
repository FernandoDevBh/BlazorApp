using Microsoft.JSInterop;
using Client.Containers.State;
using Client.Services.Contracts;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace Client.Pages.Card.ViewModels;

public class CardListVM : BaseViewModel, ICardListVM
{
    private bool isLoading;
    private int deleteId;
    private bool showDeleteModal;
    private readonly IJSRuntime jSRuntime;
    private readonly ICardService cardService;
    private IQueryable<ProductDTO> cards = new List<ProductDTO>(0).AsQueryable();
    private PaginationState pagination = new PaginationState { ItemsPerPage = 8 };

    public CardListVM(IJSRuntime jSRuntime, ICardService cardService)
    {
        this.jSRuntime = jSRuntime;
        this.cardService = cardService;
    }

    public bool IsLoading
    {
        get => isLoading;
        private set => SetValue(ref isLoading, value);
    }

    public IQueryable<ProductDTO> Cards
    {
        get => cards;
        private set => SetValue(ref cards, value);
    }

    public PaginationState Pagination
    {
        get => pagination;
        private set => SetValue(ref pagination, value);
    }

    public bool ShowDeleteModal
    {
        get => showDeleteModal;
        private set => SetValue(ref showDeleteModal, value);
    }

    public void SetIsLoading() => IsLoading = !IsLoading;

    public void Delete(int id)
    {
        deleteId = id;
        ShowDeleteModal = true;
    }

    public async Task LoadCards()
    {
        IsLoading = true;
        Cards = (await cardService.GetAllCardsAsync()).AsQueryable();
        IsLoading = false;
    }

    public async Task DeleteConfirmation(bool isConfirmed)
    {
        IsLoading = true;
        if (isConfirmed && deleteId != 0)
        {
            await cardService.DeleteCardAsync(deleteId);
            await LoadCards();
            await jSRuntime.ToastrSuccess("Card deleted succesfully");
        }
        showDeleteModal = false;
        IsLoading = false;
    }
}
