using System.ComponentModel.DataAnnotations;

namespace webstore.api.Dtos; 

// A data transfer object (DTO) for creating a new item in the web store. 

// I will refrain from adding an ID since ID's are usually provided by the server on creation of new items.
public class CreateItemDto
{
    [Required] [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
    
    [Required] [Range(1,500)]
    public decimal Price { get; set; } 

    [Range(1,50)] 
    public int CategoryID { get; set; }
}