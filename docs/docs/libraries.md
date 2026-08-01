# Libraries

The following projects integrate with GirCore. They are grouped by the application platform they extend. To add a new library to the list, [edit this page](https://github.com/gircore/gir.core/edit/main/docs/docs/libraries.md).

## GTK

### Nickvision.Desktop

A cross-platform library that encapsulates differences between Windows and Linux systems through C# service classes.

- [GitHub](https://github.com/nickvisionapps/desktop)
- [NuGet](https://www.nuget.org/packages/Nickvision.Desktop)

### Nickvision.Desktop.GNOME

A library containing GirCore extensions and helpers for using Blueprint files in C# projects.

- [GitHub](https://github.com/nickvisionapps/desktop)
- [NuGet](https://www.nuget.org/packages/Nickvision.Desktop.GNOME)

### SkiaSharp.Views.GirCore

A library that provides GTK4 and Cairo-backed views for using SkiaSharp on Linux with GirCore.

- [GitHub](https://github.com/mono/SkiaSharp/tree/main/source/SkiaSharp.Views/SkiaSharp.Views.Gtk4)
- [NuGet](https://www.nuget.org/packages/SkiaSharp.Views.Gtk4/)

## .NET MAUI

### Linux GTK4 backend

The experimental Linux GTK4 backend for .NET MAUI uses GirCore bindings to render MAUI controls as native GTK4 widgets.

- [GitHub](https://github.com/dotnet/maui-labs/tree/main/platforms/Linux.Gtk4)
- [Documentation](https://learn.microsoft.com/dotnet/maui/developer-tools/platform-backends/linux-gtk4)
- [NuGet](https://www.nuget.org/packages/Microsoft.Maui.Platforms.Linux.Gtk4/)

## Tooling

### Nickvision.FlatpakGenerator

A tool for generating a `nuget-sources.json` file that can be included in an application's Flatpak build.

- [GitHub](https://github.com/nickvisionapps/flatpakgenerator)
- [NuGet](https://www.nuget.org/packages/Nickvision.FlatpakGenerator)
