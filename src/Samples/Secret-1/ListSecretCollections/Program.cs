using System;

Secret.Module.Initialize();

var service = Secret.Service.GetSync(Secret.ServiceFlags.LoadCollections, null);
var collections = service.GetCollections() ?? throw new Exception("No collections found");

Console.WriteLine("Secret collections:");
GLib.List.Foreach(collections, data =>
{
    var collection = (Secret.Collection) GObject.Internal.InstanceWrapper.WrapHandle<Secret.Collection>(data, false);
    Console.WriteLine($" - {collection.Label}");
});

