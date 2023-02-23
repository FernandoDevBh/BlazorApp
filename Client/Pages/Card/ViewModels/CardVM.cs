using Client.Containers.State;
using Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

namespace Client.Pages.Card.ViewModels;

public class CardVM : BaseViewModel
{
    private bool isLoading = false;

    private int id;
    private string name;
    private string description;
    private bool shopFavorites;
    private bool customerFavorites;
    private string color;
    private string imageUrl;
    private int categoryId;
    private ICollection<ProductPriceDTO> productPrices;

    private readonly IJSRuntime jsRuntime;
    private readonly ICardService cardService;
    private readonly NavigationManager navigationManager;

    public CardVM(IJSRuntime jsRuntime, ICardService cardService, NavigationManager navigationManager)
    {
        this.jsRuntime = jsRuntime;
        this.cardService = cardService;
        this.navigationManager = navigationManager;        
        name = string.Empty;
        description = string.Empty;
        color = string.Empty;
        imageUrl = string.Empty;
        productPrices = new HashSet<ProductPriceDTO>();
    }

    public bool IsLoading
    {
        get => isLoading;
        set => SetValue(ref isLoading, value);
    }

    public int Id
    {
        get => id;
        set => SetValue(ref id, value);
    }

    public string Name
    {
        get => name;
        set => SetValue(ref name, value);
    }

    public string Description
    {
        get => description;
        set => SetValue(ref description, value);
    }

    public bool ShopFavorites
    {
        get => shopFavorites;
        set => SetValue(ref shopFavorites, value);
    }

    public bool CustomerFavorites
    {
        get => customerFavorites;
        set => SetValue(ref customerFavorites, value);
    }

    public string Color
    {
        get => color;
        set => SetValue(ref color, value);
    }

    public string ImageUrl
    {
        get => imageUrl;
        set => SetValue(ref imageUrl, value);
    }

    public int CategoryId
    {
        get => categoryId;
        set => SetValue(ref categoryId, value);
    }

    public ICollection<ProductPriceDTO> ProductPrices
    {
        get => productPrices;
        set => SetValue(ref productPrices, value);
    }

    public async Task LoadCategory()
    {
        IsLoading = true;
        Reset();
        Name = string.Empty;
        if (Id == 0)
        {
            IsLoading = false;
        }
        else
        {
            var card = await cardService.GetCardByIdAsync(Id);
            Id = card.Id;
            CategoryId = card.CategoryId;
            
            if(!string.IsNullOrEmpty(card.Name)) Name = card.Name;
            if(!string.IsNullOrEmpty(card.Description)) Description = card.Description;
            if(!string.IsNullOrEmpty(card.Color)) Color = card.Color;
            if (!string.IsNullOrEmpty(card.ImageUrl)) ImageUrl = card.ImageUrl;

            ShopFavorites = card.ShopFavorites;
            CustomerFavorites = card.CustomerFavorites;                        
            ProductPrices = card.ProductPrices;
        }
    }

    private void Reset()
    {
        Id = 0;
        CategoryId = 0;
        Name = string.Empty;
        Description = string.Empty;
        ShopFavorites = false;
        CustomerFavorites = false;
        Color = string.Empty;
        ImageUrl = string.Empty;
        ProductPrices = new HashSet<ProductPriceDTO>();
    }
}
