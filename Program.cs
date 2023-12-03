using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using RestaurantNamespace;


// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:5122");
var client = new Catalogue.CatalogueClient(channel);

// Assuming the RestaurantRequest and RestaurantResponse messages are generated
var restaurantId = 1; // Replace with the desired restaurant ID
var request = new RestaurantRequest { RestaurantId = restaurantId };

try
{
    var response = await client.SendCatalogueAsync(request);
    var restaurant = response.Restaurant;

    Console.WriteLine($"Restaurant ID: {restaurant.Id}");

    foreach (var menuItem in restaurant.Menu)
    {
        Console.WriteLine($"Menu Item ID: {menuItem.Id}, Name: {menuItem.Name}, Price: {menuItem.Price}");
    }
}
catch (RpcException ex)
{
    Console.WriteLine($"RPC Error: {ex.Status.Detail}");
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();