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

// Get a specific item by id(4)
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
    // The http 201 status code tells us a request has led to the creation of a resource.
    return Results.CreatedAtRoute("GetItem", new {id = item.ItemID}, item);
});


// PUT operation items/{anyid} currently using 4
// expression knows it will get id from url, so this is for UpdateItemDto method

app.MapPut("/items/{id}", (int id, UpdateItemDto updatedItem) => 
{
   var index = items.FindIndex(item => item.ItemID == id);  

   items[index] = new ItemDto
   {
    ItemID = id,
    Name = updatedItem.Name,
    Price = updatedItem.Price
   };

   // NoContent 204 status code means a succesful request and the client does not need to navigate away from its current page.
   return Results.NoContent();
});


// Delete operation items/{anyid} but currently using 4
// We have to provide the id for deletion
// The Deletion will happen in request wheter the item exist or not.
app.MapDelete("/items/{id}", (int id) =>
{
  items.RemoveAll(item => item.ItemID == id);  

  // Same reason as in Put operation.
  return Results.NoContent();
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
