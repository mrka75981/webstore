namespace webstore.api.Dtos;

public class ItemDetailsDto
{
    public int ItemID { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryID { get; set; }
}