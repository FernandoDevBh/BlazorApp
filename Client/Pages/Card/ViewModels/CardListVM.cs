using Microsoft.JSInterop;
using Client.Containers.State;
using Client.Services.Contracts;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace Client.Pages.Card.ViewModels;

public class CardListVM : BaseViewModel, ICardListVM
{
    private int setId;
    private bool isLoading;
    private int deleteId;
    private bool showDeleteModal;
    private string setName = string.Empty;
    private readonly IJSRuntime jSRuntime;
    private readonly ICardService cardService;
    private readonly ISetService setService;
    private IQueryable<ProductDTO> cards = new List<ProductDTO>(0).AsQueryable();    


    public CardListVM(IJSRuntime jSRuntime, ICardService cardService, ISetService setService)
    {
        this.jSRuntime = jSRuntime;
        this.cardService = cardService;
        this.setService = setService;
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

    public bool ShowDeleteModal
    {
        get => showDeleteModal;
        private set => SetValue(ref showDeleteModal, value);
    }
    public int SetId { get => setId; set => SetValue(ref setId, value); }
    public string SetName { get => setName; set => SetValue(ref setName, value); }

    public void SetIsLoading() => IsLoading = !IsLoading;

    public void Delete(int id)
    {
        deleteId = id;
        ShowDeleteModal = true;
    }

    public async Task LoadCards()
    {
        IsLoading = true;
        var set = await setService.GetById(SetId);
        SetName = $"Card List {set.Name}";
        Cards = (await cardService.GetAllCardsAsync(SetId)).AsQueryable();
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
