# Welcome to Gir.Core

[![Continuous integration status](https://github.com/GirCore/gir.core/workflows/Continuous%20integration/badge.svg?branch=develop)](https://github.com/gircore/gir.core/actions)
[![Website depolyment status](https://github.com/GirCore/gircore.github.io/workflows/Deploy%20website/badge.svg?branch=develop)](https://github.com/gircore/gircore.github.io/actions)

Gir.Core is a project which aims to provide C# wrapper for different [GObject] based libraries like [GTK] for user interfaces.

<!-- If you want to get started with the library head over to http://gircore.tiede.org. If you want to get into more technical details just continue reading. -->

For the [GObject] system there are a lot of libraries which allow to write complete applications with deep system integration on linux. Unfortunately there are only bindings for [GtkSharp] which are well maintained and they just wrap the low level C-API.

This project aims to provide:
* An API which feels more natural to C# developers and thus simplifies the C-API (including the async/await feature).
* A more complete API surface to integrate deeply with linux via supporting more libraries
* A declarative style of creating [GTK] UIs. See the [sample][sample_gtk_quickstart]. (Additionally to the XML and object based possibilities to create UIs).
* An easy way to allow 3rd party developers to integrate into this stack to achieve interoperability between different [GObject] based libraries.

Currently supported libraries
* [GTK] (partial support): UI-Toolkit (GTK3 + experimental GTK4)
* [GStreamer] (partial support): Multimedia Framework
* [DBus] (partial support): Library for inter-process communication via [GIO]
* [WebKitGTK] (planned): Browser-Engine
* [JavaScriptCore] (planned): Javascript integration for [WebKitGTK]
* [libhandy] (planned): Convergent UI-Widgets for [GTK] to support mobile phones
* [libchamplain] (planned): Library to display maps
* [GdkPixbuf] (partial support): Load images in various formats

## Status
As we are currently figuring out the best way for this project the code is under heavy development and not ready for production. There is currently _no_ nuget package available.

## Build & Use
To build the project locally in debug mode follow these steps:

    $ git clone --recursive https://github.com/gircore/gir.core.git
    $ cd gir.core/Build
    $ dotnet run

### Options

There are some options which can be used to influence the code generation:

* `--release`: Execute the targets with the Release configuration. If not specified the Debug configuration is used.
* `--xml-documentation`: Generate the xml documentation.
* `--generate-comments`: Take over comments from gir file into the wrapper code. Be aware of the LGPL license of the comments.
* `--targets <targets>`: A list of targets to run or list.
* `--version <version>`: Specify the version number of the `build`.
* `--disable-async`: Runs the generator synchronously (useful for debugging if something goes wrong)

To get a full list of available options use `--help`.

### Targets

Supported targets are:
* `generate`: Generates the source code files. Recognizes `comments` option.
* `build`: Builds the project with `Debug` or `Release` configuration. Recognizes `xml-documentation` and `version` option. Depends on `generate` target.
* `integration`: Builds the integration library.
* `unittest`: Execute unit tests with `Debug` or `Release` configuration. Depends on `build`.
* `integrationtest`: Execute integrations tests with `Debug` or `Release` configuration. Depends on `unittest`.
* `pack`: Packs the libraries into the `Nuget` folder in the project root. Recognizes `version` option. Depends on `build`.
* `clean`:  Cleans `samples` and `build` output including generated source code files.
* `samples`: Builds the sample applications with `Debug` or `Release` configuration. Depends on `build` and `integration`.

If no target is specified the `build` target is executed.

### Examples

If you want to clean your debug build just run:

    $ dotnet run -- --targets clean

If you want to generate the xml documentation, build the samples and run the test cases in debug mode just run:

    $ dotnet run -- --xml-documentation --targets test samples

If you want to build the wrappers in release mode just run

    $ dotnet run -- --release

To use the newly build libraries in your project just add a reference to the csproj file of the project you want to use, e.g:

    $ dotnet add reference [RepoPath]/Libs/Gtk/Gtk.csproj

## How to help
Anyone who wants to help is very welcome. If you want to get into the project take a look at our [Good First Issues](https://github.com/gircore/gir.core/issues?q=is%3Aissue+is%3Aopen+label%3A%22good+first+issue%22) or get in contact by starting a [Discussion](https://github.com/gircore/gir.core/discussions).

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
[sample_gtk_quickstart]: https://github.com/gircore/gir.core/tree/develop/Samples/Gtk3/QuickStart
[GdkPixbuf]: https://gitlab.gnome.org/GNOME/gdk-pixbuf

## Licensing terms
Gir.Core is licensed under the terms of the MIT-License. Please see the [LICENSE](LICENSE) file for further information.

The [Gir.Core logo](logo.svg) is built upon the [original GTK logo](https://wiki.gnome.org/Projects/GTK/Logo) by Andreas Nilsson which is licensed under the [GNU Free Documentation License](https://www.gnu.org/licenses/fdl-1.3.txt) and was relicensed under CC BY-SA 3.0. Therefore the Gir.Core logo is licensed under the [CC BY-SA 3.0](https://creativecommons.org/licenses/by-sa/3.0/deed.en), too.
