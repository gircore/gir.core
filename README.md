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
* [libhandy] (planned): Convergent UI-Widgets for [GTK] to support mobile phones

[GObject]: https://developer.gnome.org/gobject/stable/
[GTK]: https://gtk.org/
[libhandy]: https://source.puri.sm/Librem5/libhandy
[WebKitGTK]: https://webkitgtk.org/
[JavaScriptCore]: https://webkitgtk.org/reference/jsc-glib/stable/index.html
