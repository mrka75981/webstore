using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using webstore.api.Dtos;
using webstore.api.EndPoints;

// init
var builder = WebApplication.CreateBuilder(args);

// Validation services for correct input

builder.Services.AddValidation(); 

// Adding CORS and policy to frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy
        .WithOrigins("http://localhost:5168")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("DevCors");

app.MapItemsEndpoints();


app.Run(); 





