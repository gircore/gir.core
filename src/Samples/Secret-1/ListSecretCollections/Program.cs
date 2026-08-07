using System;

Secret.Module.Initialize();

var service = Secret.Service.GetSync(Secret.ServiceFlags.LoadCollections, null);
var collections = service.GetCollections() ?? throw new Exception("No collections found");

Console.WriteLine("Secret collections:");
foreach (var collection in collections)
{
    Console.WriteLine($" - {collection.Label}");
}

