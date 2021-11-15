<img src="https://raw.githubusercontent.com/gircore/gir.core/develop/img/logo.svg" align="right" />

# Welcome to Gir.Core

[![Continuous integration status](https://github.com/GirCore/gir.core/workflows/Continuous%20integration/badge.svg?branch=develop)](https://github.com/gircore/gir.core/actions)
[![Website depolyment status](https://github.com/GirCore/gircore.github.io/workflows/Deploy%20website/badge.svg?branch=develop)](https://github.com/gircore/gircore.github.io/actions)

Gir.Core is a C# wrapper for [GObject]-based libraries like [GTK] for user interfaces.

This project aims to provide a complete set of APIs for writing rich cross-platform user interfaces and multimedia programs. It is built upon the well-established [GObject Introspection][gi] framework for language bindings.

### Features
* **Idiomatic C#:** An API which feels natural to C# developers (including the async/await feature).
* **Simplicity:** Memory management is handled automatically, greatly simplifying the C-API.
* **Complete API:** Support for the entire GTK and GStreamer stack, enabling feature-rich applications which deeply integrate with the OS.
* **Declarative UI:** A declarative syntax for creating GTK UIs (See the [DeclarativeUI Sample][sample_gtk_declarative]). User interfaces may also be created the traditional way and/or with [GtkBuilder XML][GtkBuilder].
* **Extensibility:** Allows 3rd party developers to write bindings for other GObject-based libraries, achieving full interoperability between them.

## Status
We are currently in a period of heavy iteration over the core internals of the project. The code is under heavy development and not ready for production. There is currently _no_ nuget package available.

### Supported Libraries

| Library        | Description                             | Level of Support  |
|----------------|-----------------------------------------|-------------------|
| GTK 3          | UI-Toolkit                              | Partial           |
| GStreamer      | Multimedia Framework                    | Partial           |
| Cairo          | Graphics Library                        | Partial           |
| Pango          | Font/Text Library                       | Partial           |
| DBus           | Library for inter-process communication | Partial (via GIO) |
| GdkPixbuf      | Image loading in various formats        | Partial           |
| GTK 4          | UI-Toolkit                              | Planned           |
| libhandy       | Convergent UI for GTK on Mobile         | Planned           |
| libchamplain   | Library to display maps                 | Planned           |
| WebKitGTK      | Browser Engine                          | Planned           |
| JavaScriptCore | JavaScript engine for WebKit            | Planned           |


## Get Involved
Anyone who wants to help is very welcome. If you want to start working on the project, take a look at our [Good First Issues](https://github.com/gircore/gir.core/issues?q=is%3Aissue+is%3Aopen+label%3A%22good+first+issue%22) or get in touch by starting a [Discussion](https://github.com/gircore/gir.core/discussions).

### Matrix Room
We have a matrix room for discussing gir.core. Please join if you'd like to help (or just want to chat!)

https://matrix.to/#/#gircore:matrix.org?via=matrix.org

## Build & Use
To build the project locally in debug mode follow these steps. Make sure to initialise submodules with `--recursive` otherwise the `gir-files` directory will not be loaded properly.

```sh
$ git clone --recursive https://github.com/gircore/gir.core.git
$ cd gir.core/src/Generation/Build
$ dotnet run
```
If you want to build using Windows please see the accompanying [documentation](docs/windows.md).

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
* `systemtest`: Execute integration tests that require system services like the DBus System Bus or Wayland Display Server. Depends on `integrationtest`.
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

## Code structure
The folder structure in this repository is organized as follows:
* **[src/Generation/Build](src/Generation/Build):** The build tool determines which projects to build and generates the libraries. Everything works automatically.
* **[src/Generation/GirLoader](src/Generation/GirLoader):** A library for reading and resolving GObject Introspection repositories.
* **[src/Generation/Generator](src/Generation/Generator):** Code generator generates C# code from GObject Introspection data.
* **[src/Integration](src/Integration):** Optional source generators to reduce boilerplate code in your projects.
* **[src/Libs](src/Libs):** Contains manually written code for libraries. The generator outputs code here.
* **[src/Samples](src/Samples):** Example programs using GTK, GStreamer, and others.
* **[src/Tests](src/Tests):** Unit and Integration tests.
* **[ext/gir-files](https://github.com/gircore/gir-files):** Introspection data from [gircore/gir-files](https://github.com/gircore/gir-files).

The code in the library folder is not complete because most of the code is generated when the build tool is run.

[gi]: https://gi.readthedocs.io/
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
[sample_gtk_declarative]: https://github.com/gircore/gir.core/blob/develop/Samples/Gtk3/DeclarativeUi/Program.cs
[GdkPixbuf]: https://gitlab.gnome.org/GNOME/gdk-pixbuf
[GtkBuilder]: https://developer.gnome.org/gtk3/stable/GtkBuilder.html

## Licensing terms
Gir.Core is licensed under the terms of the MIT-License. Please see the [license file](license.txt) for further information.

The [Gir.Core logo](img/logo.svg) is built upon the [original GTK logo](https://wiki.gnome.org/Projects/GTK/Logo) by Andreas Nilsson which is licensed under the [GNU Free Documentation License](https://www.gnu.org/licenses/fdl-1.3.txt) and was relicensed under CC BY-SA 3.0. Therefore the Gir.Core logo is licensed under the [CC BY-SA 3.0](https://creativecommons.org/licenses/by-sa/3.0/deed.en), too.
