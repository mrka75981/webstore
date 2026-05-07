using System.ComponentModel.DataAnnotations;

namespace webstore.api.Dtos;

public class UpdateItemDto
{
    [Required] [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
    
    [Required] [Range(1,500)]
    public decimal Price { get; set; }
}