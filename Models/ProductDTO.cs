using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class ProductDTO
{
    public ProductDTO()
    {
        ProductPrices = new List<ProductPriceDTO>(0);
        Category = new();
    }

    public int Id { get; set; }
    
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }
    public bool ShopFavorites { get; set; }
    public bool CustomerFavorites { get; set; }
    public string? Color { get; set; }
    public string? ImageUrl { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
    public int CategoryId { get; set; }    
    public CategoryDTO Category { get; set; }

    public ICollection<ProductPriceDTO> ProductPrices { get; set; }
}
