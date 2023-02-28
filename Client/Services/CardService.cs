using Client.Services.Contracts;

namespace Client.Services;

public class CardService : ICardService
{
    private readonly HttpClient httpClient;

    public CardService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ICollection<ProductDTO>> GetAllCardsAsync(int categoryId)
    {
        var response = await httpClient.GetAsync($"product/{categoryId}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<ICollection<ProductDTO>>();

        if (response.IsSuccessStatusCode)
            return responseDTO;
        else
            throw new Exception("Error on GetAllCardsAsync");
    }

    public async Task<ProductDTO> GetCardByIdAsync(int setId, int id)
    {
        var response = await httpClient.GetAsync($"product/{setId}/{id}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<ProductDTO>();

        if (response.IsSuccessStatusCode)
            return responseDTO;
        else
            throw new Exception("Error on GetCardByIdAsync");
    }

    public async Task<ProductDTO> AddCardAsync(ProductDTO card)
    {
        var bodyContent = card.GetStringContent();
        var response = await httpClient.PostAsync("product", bodyContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<ProductDTO>();

        if (response.IsSuccessStatusCode)
            return responseDTO;
        else
            throw new Exception("Error on AddCardAsync");
    }

    public async Task<ProductDTO> UpdateCardAsync(ProductDTO card)
    {
        var bodyContent = card.GetStringContent();
        var response = await httpClient.PutAsync($"product/{card.Id}", bodyContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<ProductDTO>();

        if (response.IsSuccessStatusCode)
            return responseDTO;
        else
            throw new Exception("Error on UpdateCardAsync");
    }

    public async Task<bool> DeleteCardAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"product/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<ICollection<ProductPriceDTO>> GetAllCardPricesAsync(int productId)
    {
        var response = await httpClient.GetAsync($"product/{productId}/prices");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<ICollection<ProductPriceDTO>>();

        if (response.IsSuccessStatusCode)
            return responseDTO;
        else
            throw new Exception("Error on GetAllCardsAsync");
    }

    public async Task<ProductPriceDTO> GetCardPriceByIdAsync(int productId, int priceId)
    {
        var response = await httpClient.GetAsync($"product/{productId}/prices/{priceId}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<ProductPriceDTO>();

        if (response.IsSuccessStatusCode)
            return responseDTO;
        else
            throw new Exception("Error on GetAllCardsAsync");
    }

    public async Task<ProductPriceDTO> AddCardPriceAsync(ProductPriceDTO productPrice)
    {
        var bodyContent = productPrice.GetStringContent();
        var response = await httpClient.PostAsync($"product/{productPrice.ProductId}/prices", bodyContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<ProductPriceDTO>();

        if (response.IsSuccessStatusCode)
            return responseDTO;
        else
            throw new Exception("Error on AddCardPriceAsync");
    }

    public async Task<ProductPriceDTO> UpdateCardPriceAsync(ProductPriceDTO productPrice)
    {
        var bodyContent = productPrice.GetStringContent();
        var response = await httpClient.PutAsync($"product/{productPrice.ProductId}/prices/{productPrice.Id}", bodyContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<ProductPriceDTO>();

        if (response.IsSuccessStatusCode)
            return responseDTO;
        else
            throw new Exception("Error on UpdateCardPriceAsync");
    }

    public async Task<bool> DeleteCardPriceByIdAsync(int productId, int id)
    {
        var response = await httpClient.DeleteAsync($"product/{productId}/prices/{id}");
        return response.IsSuccessStatusCode;
    }
}
