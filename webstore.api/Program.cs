using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using webstore.api.Dtos;

// init
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string GetItemEndpointName = "GetItem";
List<ItemDto> items = new List<ItemDto>(); 

// sample data
InitializeItems();

// Get items
app.MapGet("/items", () => items);

// Get a specific item by id
app.MapGet("/items/{id}", (int id) => items.Find(item => item.ItemID == id)).WithName(GetItemEndpointName);

// Post a new item
app.MapPost("/items", (CreateItemDto newItem) =>
{
    ItemDto  item = new ItemDto
    {
        ItemID = items.Count + 1, 
        Name = newItem.Name,
        Price = newItem.Price
    };
    items.Add(item);

    return Results.CreatedAtRoute("GetItem", new {id = item.ItemID}, item);
});

app.Run(); 




void InitializeItems()
{
    ItemDto Lasagne = new ItemDto
    {
        ItemID = 1,
        Name = "lasagne",
        Price = 10.99m
    };  

    ItemDto Bread = new ItemDto
    {
        ItemID = 2,
        Name = "bread",
        Price = 2.99m
    }; 

    ItemDto Spaghetti = new ItemDto
    {
        ItemID = 3,
        Name = "spaghetti",
        Price = 8.99m
    };

    items.Add(Lasagne);
    items.Add(Bread);
    items.Add(Spaghetti);
}
