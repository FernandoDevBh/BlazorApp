using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess;

public class Product
{
    public Product()
    {
        ProductPrices = new List<ProductPrice>();
        Category = new();
    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool ShopFavorites { get; set; }
    public bool CustomerFavorites { get; set; }
    public string? Color { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }

    public ICollection<ProductPrice> ProductPrices { get; set; }
}
