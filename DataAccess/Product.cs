using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess;

public class Product
{    
    public int Id { get; set; }    
    public string? Name { get; set; }
    public int Number { get; set; }
    public bool InMyCollection { get; set; }
    public string? Image { get; set; }
    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;
}
