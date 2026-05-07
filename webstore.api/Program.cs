using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using webstore.api.Dtos;
using webstore.api.EndPoints;

// init
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapItemsEndpoints();


app.Run(); 





