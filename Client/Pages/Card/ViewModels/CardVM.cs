using Client.Containers.State;
using Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

namespace Client.Pages.Card.ViewModels;

public class CardVM : BaseViewModel, ICardVM
{
    private bool isLoading = false;
    private string title = "Create";
    private int id;
    private string name;
    private int number;
    private bool inMyCollection;
    private string image;
    private int categoryId;

    private readonly IJSRuntime jsRuntime;
    private readonly ICardService cardService;
    private readonly NavigationManager navigationManager;

    public CardVM(IJSRuntime jsRuntime, ICardService cardService, NavigationManager navigationManager)
    {
        this.jsRuntime = jsRuntime;
        this.cardService = cardService;
        this.navigationManager = navigationManager;
        title = "Create";
        name = string.Empty;
        image = string.Empty;
    }

    public bool IsLoading { get => isLoading; set => SetValue(ref isLoading, value); }
    public int Id { get => id; set => SetValue(ref id, value); }

    [Required()]
    public string Name { get => name; set => SetValue(ref name, value); }

    [Required()]
    public int CategoryId { get => categoryId; set => SetValue(ref categoryId, value); }
    public bool InMyCollection { get => inMyCollection; set => SetValue(ref inMyCollection, value); }

    [Required()]
    [Range(1, int.MaxValue, ErrorMessage = "Card Number is required")]
    public int Number { get => number; set => SetValue(ref number, value); }

    [Required()]
    public string Image { get => image; set => SetValue(ref image, value); }
    public string Title { get => title; set => SetValue(ref title, value); }

    public async Task LoadCard()
    {
        IsLoading = true;
        Reset();        
        if (Id == 0)
        {
            IsLoading = false;
        }
        else
        {
            var card = await cardService.GetCardByIdAsync(CategoryId,Id);
            Id = card.Id;            
            Name = card.Name!;
            InMyCollection = card.InMyCollection;
            Number = card.Number;
            Image = card.Image!;
            IsLoading = false;
        }
    }

    private void Reset()
    {        
        title = "Create";

        if(Id > 0)        
            title = "Edit";
        
        Name = string.Empty;
        InMyCollection = false;
        Number = 0;
        Image = string.Empty;
    }

    public async Task UpsertCard()
    {
        IsLoading = true;
        if (Id == 0)
        {
            await cardService.AddCardAsync(new()
            {
                CategoryId = CategoryId,
                Name = Name,
                Image = Image,
                InMyCollection = InMyCollection,
                Number = Number,
            });
        }
        else
        {
            await cardService.UpdateCardAsync(new()
            {
                Id = Id,
                Name = Name,                
                Image = Image,
                InMyCollection = InMyCollection,
                Number = Number,
                CategoryId = CategoryId,
            });
        }
        navigationManager.NavigateTo($"card/{CategoryId}");        
        IsLoading = false;
        await jsRuntime.ToastrSuccess("Card process with success");
        Reset();
    }

    public async Task HandleImageUpload(InputFileChangeEventArgs e)
    {
        try
        {
            Image = await ImageHelper.GetImageDataAsync(e);
        }
        catch (Exception exception)
        {
            await jsRuntime.ToastrError(exception.Message);
        }
    }
}
