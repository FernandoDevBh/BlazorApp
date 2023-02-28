using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class ProductDTO
{    
    public int Id { get; set; }
    
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public int Number { get; set; }

    [Required]
    public bool InMyCollection { get; set; }

    [Required]
    public string? Image { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
    public int CategoryId { get; set; }
}
