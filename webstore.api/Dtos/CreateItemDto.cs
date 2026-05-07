namespace webstore.api.Dtos; 

// A data transfer object (DTO) for creating a new item in the web store. 

// I will refrain from adding an ID since ID's are usually provided by the server on creation of new items.
public class CreateItemDto
{
    public string Name { get; set; } 
    public decimal Price { get; set; }
}