# Welcome to Gir.Core

Gir.Core is a project which aims to provide C# wrapper for different [GObject] based libraries like [GTK] for user interfaces.

<!-- If you want to get started with the library head over to http://gircore.tiede.org. If you want to get into more technical details just continue reading. -->

For the [GObject] system there are a lot of libraries which allow to write complete applications with deep system integration on linux. Unfortunately there are only bindings for [GtkSharp] which are well maintained and they just wrap the low level C-API.

This project aims to provide:
* An API which feels more natural to C# developers and thus simplifies the C-API (including the async/await feature).
* A more complete API surface to integrate deeply with linux via supporting more libraries
* A declarative style of creating [GTK] UIs. See the [sample][sample_gtk_quickstart]. (Additionally to the XML and object based possibilities to create UIs).
* An easy way to allow 3rd party developers to integrate into this stack to achieve interoperability between different [GObject] based libraries.

Currently supported libraries
* [GTK] (wip): UI-Toolkit (GTK3 + experimental GTK4)
* [WebKitGTK] (planed): Browser-Engine
* [JavaScriptCore] (planed): Javascript integration for [WebKitGTK]
* [libhandy] (planed): Convergent UI-Widgets for [GTK] to support mobile phones
* [dbus] (wip): Library for inter-process communication via [GIO]
* [libchamplain] (planed): Library to display maps
* [gstreamer] (wip): Multimedia Framework

## Status
As we are currently figuring out the best way for this project the code is under heavy development and not ready for production. There is currently _no_ nuget package available.

## Build & Use
To build the project locally follow these steps:

    $ git clone --recursive https://github.com/gircore/gir.core.git
    $ cd Build
    $ dotnet run -- release build

If you want to create a debug build just run

    $ dotnet run -- debug build
    
If you want to clean your debug build just run

    $ dotnet run -- debug clean

If you want to build the samples just run

    $ dotnet run -- debug samples
    
To use the newly build libraries in your project just add a reference to the csproj file of the project you want to use, e.g:

    $ dotnet add reference [RepoPath]/Libs/Gtk/Gtk.csproj

## How to help
Anyone who wants to help is very welcome. Just create a pull request for new code or create an issue to get in contact.

## Code structure
The folder structure in this repository is organized like:
* **Build:** Run the project to generate the libraries. Everything works automatically.
* **Generator:** Code generator to create the lower API layer
* **Samples:** Example apps for GTK, gstreamer, ...
* **Libs:** Contains the libraries

The code in the library folder is not complete because the biggest part of the code gets generated and is only available after a run of the build project.

[gstreamer]: https://gstreamer.freedesktop.org/
[GIO]: https://developer.gnome.org/gio/stable/
[GObject]: https://developer.gnome.org/gobject/stable/
[GTK]: https://gtk.org/
[libhandy]: https://source.puri.sm/Librem5/libhandy
[WebKitGTK]: https://webkitgtk.org/
[JavaScriptCore]: https://webkitgtk.org/reference/jsc-glib/stable/index.html
[dbus]: https://www.freedesktop.org/wiki/Software/dbus/
[libchamplain]: https://wiki.gnome.org/Projects/libchamplain
[GtkSharp]: https://github.com/GtkSharp/GtkSharp
[sample_gtk_quickstart]: https://github.com/gircore/gir.core/tree/develop/Samples/Gtk3/Quickstart
