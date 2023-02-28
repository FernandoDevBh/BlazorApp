namespace Client.Services.Contracts;

public interface ICardService
{
    Task<ICollection<ProductDTO>> GetAllCardsAsync(int categoryId);
    Task<ProductDTO> GetCardByIdAsync(int setId, int id);
    Task<ProductDTO> AddCardAsync(ProductDTO card);
    Task<ProductDTO> UpdateCardAsync(ProductDTO card);    
    Task<bool> DeleteCardAsync(int id);
    Task<ICollection<ProductPriceDTO>> GetAllCardPricesAsync(int productId);
    Task<ProductPriceDTO> GetCardPriceByIdAsync(int productId, int priceId);
    Task<ProductPriceDTO> AddCardPriceAsync(ProductPriceDTO productPrice);
    Task<ProductPriceDTO> UpdateCardPriceAsync(ProductPriceDTO productPrice);
    Task<bool> DeleteCardPriceByIdAsync(int productId, int id);
}
