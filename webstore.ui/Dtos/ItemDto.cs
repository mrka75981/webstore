namespace webstore.ui.Dtos;


/* A Data Transfer Object (DTO) for an item in the web store.
This class is used to transfer data between the client and the server. 

It is like a contract between the client and the server on how the data will be used.*/

public class ItemDto
{
    public int ItemID { get; set; }
    public string Name { get; set; } = string.Empty; 
    public decimal Price { get; set; }
}