using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using webstore.api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<ItemDto> items = new List<ItemDto>(); 

InitializeItems();

app.MapGet("/items", () => items);

app.MapGet("/items/{id}", (int id) => items.Find(item => item.ItemID == id));

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
