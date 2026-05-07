namespace webstore.api.Models;

public class Item
{
    public int ItemID { get; set; }
    public required string Name { get; set; } 
    
    public decimal Price { get; set; } 

    public int CategoryID { get; set; }
}