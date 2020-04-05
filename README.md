# Welcome to Gir.Core

Gir.Core is a project which aims to provide C# wrapper for different [GObject] based libraries like [GTK] for user interfaces.

The foucs is not to just wrap the libraries:
* Create a simple and streamlined C# API on top of the low level C API
* Provide an online documentation for the C# API

The long term goal is to create libraries which allow to easily convert C# MVVM applications from Windows to Linux.

Supported libraries
* [GTK] (wip): UI-Toolkit
* [WebKitGTK] (wip): Browser-Engine
* [JavaScriptCore] (wip): Javascript integration for [WebKitGTK]
* [libhandy] (wip): Convergent UI-Widgets for [GTK] to support mobile phones
* [dbus] (planned): Library for inter-process communication
* [libchamplain] (planned): Library to display maps

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
          Title = Property<string>("title",
              get: GetStr, 
              set: Set
          );

          ShowCloseButton = Property<bool>("show-close-button",
              get: GetBool,
              set: Set
          );
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

## How to help
Anyone who wants to help is very welcome. Just create a pull request for new code or create an issue to get in contact.

[GObject]: https://developer.gnome.org/gobject/stable/
[GTK]: https://gtk.org/
[libhandy]: https://source.puri.sm/Librem5/libhandy
[WebKitGTK]: https://webkitgtk.org/
[JavaScriptCore]: https://webkitgtk.org/reference/jsc-glib/stable/index.html
[dbus]: https://www.freedesktop.org/wiki/Software/dbus/
[libchamplain]: https://wiki.gnome.org/Projects/libchamplain
