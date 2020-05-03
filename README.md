# Welcome to Gir.Core

Gir.Core is a project which aims to provide C# wrapper for different [GObject] based libraries like [GTK] for user interfaces.

If you want to get started with the library head over to http://gircore.tiede.org. If you want to get into more technical details just continue reading.

For the [GObject] system there are a lot of libraries which allow to write complete applications with deep system integration on linux. Unfortunately there are only bindings for [GtkSharp] which are well maintained and they just wrap the low level C-API.

This project aims to provide:
* An API which feels more natural to C# developers and thus greatly simplifies the C-API.
* A more complete API surface to integrate deeply with linux via supporting more libraries

Supported libraries
* [GTK] (wip): UI-Toolkit
* [WebKitGTK] (wip): Browser-Engine
* [JavaScriptCore] (wip): Javascript integration for [WebKitGTK]
* [libhandy] (wip): Convergent UI-Widgets for [GTK] to support mobile phones
* [dbus] (planned): Library for inter-process communication
* [libchamplain] (wip): Library to display maps

## How is it done?
The API is split in two layers. The lower layer just wraps all the methods (like `Gtk.HeaderBar.@new()`) and is completely generated.

The high level API is currently handcrafted to be able to provide the flexiblity for an easy to use API surface. For example the binding code for the GTK HeaderBar looks like:

```cs
 public class GHeaderBar : GContainer
  {
      public Property<string> Title { get; }
      public Property<bool> ShowCloseButton { get; }

      public GHeaderBar() : this(Gtk.HeaderBar.@new()){}
      internal GHeaderBar(IntPtr handle) : base(handle) 
      {
          Title = PropertyOfString("title");
          ShowCloseButton = PropertyOfBool("show-close-button");
      }
  }
```


## Code structure
The folder structure in this repository is organized like:
* **Build:** Run the project to generate the libraries. Everything works automatically.
* **CWrapper:** Generic code generator to create the lower API layer
* **GirCWrapper:** Adapter to map GIR data to the *CWrapper*
* **GtkApp:** Example app
* **Libs:** Contains the libraries

Each library has a folder called *Wrapper*, which contains the project to generate and build the low level API. These projects do very seldom contain code.

If there is a *Core* folder it contains the high level code.

## Build
To build the project locally follow these steps:

    $ git clone https://github.com/gircore/gir.core.git
    $ cd Build
    $ dotnet run -- release build

If you want to create a debug build just run

    $ dotnet run -- debug build
    
If you want to clean your debug build just run

    $ dotnet run -- debug clean

## How to help
Anyone who wants to help is very welcome. Just create a pull request for new code or create an issue to get in contact.

[GObject]: https://developer.gnome.org/gobject/stable/
[GTK]: https://gtk.org/
[libhandy]: https://source.puri.sm/Librem5/libhandy
[WebKitGTK]: https://webkitgtk.org/
[JavaScriptCore]: https://webkitgtk.org/reference/jsc-glib/stable/index.html
[dbus]: https://www.freedesktop.org/wiki/Software/dbus/
[libchamplain]: https://wiki.gnome.org/Projects/libchamplain
[GtkSharp]: https://github.com/GtkSharp/GtkSharp
