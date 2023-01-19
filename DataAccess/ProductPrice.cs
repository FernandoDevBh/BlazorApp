using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess;

public class ProductPrice
{
    public int Id { get; set; }

    [ForeignKey("ProductId")]
    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public string? Size { get; set; }
    public double Price { get; set; }
}
