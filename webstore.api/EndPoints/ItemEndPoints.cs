using webstore.api.Data;
using webstore.api.Dtos;
using webstore.api.Models;

namespace webstore.api.EndPoints
{
    // Using extension methods the class must be static
    public static class ItemEndPoints
    {
        private static ItemStoreRepository itemStoreRepository = new ItemStoreRepository();
        private static List<ItemDto> items {get;set;} = itemStoreRepository.GetAllItemDtos();
        
        public const string GetItemEndpointName = "GetItem";
      

    public static void MapItemsEndpoints(this WebApplication app)
        {
            /** Grouping endpoints together as a route group
            * We can then define the base part of the expression 
            * and then attach each of the endpoints into the group
            */

            var group = app.MapGroup("/items");

            // Get items
            // items is inherited
            group.MapGet("/", () => items);

            // Get a specific item by id(4)
            group.MapGet("/{id}", (int id) =>
            {
                // Capture result of Find method
                var item = items.Find(item => item.ItemID == id);

                return item is null ? Results.NotFound() : Results.Ok(item);
            })
            .WithName(GetItemEndpointName);

            // Post a new item
            group.MapPost("/", (CreateItemDto newItem) =>
            {
                /* To prevent inconsistent data 
                *  You could be tempted to do a check like this 
                *  if (string.IsNullOrEmpty(newItem.Name)){
                *     return Results.BadRequest("Name is required");})
                *  But you would have to do this for each property, 
                *  and that is verbose and tedious. 
                *  Instead we use data annotations
                */
                
                Item item = new Item
                {
                    Name = newItem.Name,
                    CategoryID = newItem.CategoryID,
                    Price = newItem.Price
                };

                itemStoreRepository.AddItemToDB(item);



                // The http 201 status code tells us a request has led to the creation of a resource.
                return Results.CreatedAtRoute("GetItem", new { id = item.ItemID }, item);
            });


            // PUT operation items/{anyid} currently using 4
            // expression knows it will get id from url, so this is for UpdateItemDto method

            group.MapPut("/{id}", (int id, UpdateItemDto updatedItem) =>
            {
                var index = items.FindIndex(item => item.ItemID == id);

                if (index == -1)
                {
                    /**
                     * Could argue that this should instead create a new item
                     * Since it would satisfy the requirement of a put request
                     */
                    return Results.NotFound();
                }

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
            group.MapDelete("/{id}", (int id) =>
            {
                items.RemoveAll(item => item.ItemID == id);

                // Same reason as in Put operation.
                return Results.NoContent();
            });

        }
    }
}
