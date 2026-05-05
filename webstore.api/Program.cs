using Microsoft.AspNetCore.Server.Kestrel.Core;
using webstore.api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<ItemDto> items = new List<ItemDto>(); 

ItemDto Lasagne = new ItemDto
{
    ItemID = 1,
    Name = "lasagne",
    Price = 10.99m
}; 
items.Add(Lasagne);

app.MapGet("/items", () => items);

app.Run();
