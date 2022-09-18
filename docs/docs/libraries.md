# Libraries

Currently there are multiple libraries planned to be integrated deeply into linux: [Gtk], [WebkitGTK], [libshumate] [libadwaita], [GIO], [gstreamer].

## GTK
[Gtk] is the toolkit which is used to display windows and widgets on the screen. The widgets can be added directly in code or described through an xml file.

Supported widgets are for example: Windows, dialogs, labels, images, spinner, progressbars, several buttons and switches, textboxes, tables,  lists, menus, toolbars, popovers, and much more. It powers several linux desktops like [Gnome] and [Xfce] and applications like [Gimp].

![A picture of an example gtk application][GtkApp]

## libadwaita
[libadwaita] extends GTK with new widgets to comply to the GNOME human interface guidelines. Additionally it supports mobile devices meaning full blown applications automatically adopt their UI to different view modes, if the available space changes.

## GIO
[GIO] is a library to allow easy access to input / output operations. Currently there is initial support for [DBus] operations. DBus is a standardized IPC-Framework which all major linux desktops use for interprocess communication.

## Gstreamer
[Gstreamer] is a multimedia library to play back various media format via a flexible pipelining system. The code to playback a movie is in the [samples](https://github.com/gircore/gir.core/blob/develop/Samples/GStreamer/Play.cs).
![A picture of the Tears of Steel project played via gstreamer][GstSintel]
(Homepage of the free movie: https://mango.blender.org/)

## WebkitGTK (planned)
[WebkitGTK] is a browser component for GTK and can be used to embed the webkit webengine into an application as a widget. There is support for the web inspector and several settings to tweak the webview to your needs.

The bindings make it easy to:
* Embed javascript into a webpage
* Call a javascript function
* Callback from the webpage into the C# code.

![A picture of an example gtk application with visible webpage][GtkAppBrowser]

## libshumate (planned)
[libshumate] is map component for GTK and can be used to embed maps into an application widget. By default it uses openstreetmap.

[DBus]: https://www.freedesktop.org/wiki/Software/dbus/
[GIO]: https://developer.gnome.org/gio/stable/
[libadwaita]: https://gitlab.gnome.org/GNOME/libadwaita
[libshumate]: https://gitlab.gnome.org/GNOME/libshumate/
[WebkitGTK]: https://webkitgtk.org/
[Gtk]: https://gtk.org
[Gimp]: https://gimp.org
[Gnome]: https://gnome.org
[Xfce]: https://xfce.org
[Gstreamer]: https://gstreamer.freedesktop.org/

[GtkApp]: img/GtkApp.png "Example GtkApp"
[GstSintel]: img/GstSintel.png "Gstreamer playing back Tears of Steel (https://mango.blender.org/)"