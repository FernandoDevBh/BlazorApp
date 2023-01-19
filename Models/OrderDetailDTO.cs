using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class OrderDetailDTO
{
    public int Id { get; set; }
    [Required]
    public int OrderHeaderId { get; set; }

    [Required]
    public int ProductId { get; set; }
    public ProductDTO Product { get; set; } = null!;

    [Required]
    public int Count { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public string Size { get; set; } = string.Empty;
    [Required]
    public string ProductName { get; set; } = string.Empty;
}
