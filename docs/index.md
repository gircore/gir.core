# Welcome to GirCore
GirCore provides C# bindings for different GObject based libraries like GTK4, Adwaita, WebKitGTK, GStreamer and more. It allows to write native dotnet linux apps which integrating deeply into the system. The [applications](docs/apps.md) page lists some projects that already use GirCore.

## Display a GTK window
Displaying a native GTK4 window is straightforward. Just create a new C# project, add the `GirCore.Gtk-4.0` nuget package copy the following code into your `Program.cs` file and run the project.

[!code-csharp[](../src/Samples/Gtk-4.0/Window/Program.cs)]

There are other samples for a lot of GirCore nuget packages [available](https://github.com/gircore/gir.core/tree/main/src/Samples).

## Contributing
If you want to contribute to GirCore please check out the [github project](https://github.com/gircore/gir.core) or visit us on [matrix](https://matrix.to/#/#gircore:matrix.org?via=matrix.org).