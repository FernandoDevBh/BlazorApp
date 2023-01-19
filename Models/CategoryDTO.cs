using System.ComponentModel.DataAnnotations;

namespace Models;

public class CategoryDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }  = string.Empty;

    [Required]
    public string Symbol { get; set; } = string.Empty;
}
