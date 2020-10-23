# Building on Windows
Currently, Gtk must be obtained manually on windows. Hopefully this
can be automated down the line.

## MSYS2/GCC
The easiest way to get started on Windows is by installing gtk through msys2.

1. Download msys2 from the [official website](https://www.msys2.org/).
2. Run `pacman -Syu` to update the package database.
3. Run `pacman -S mingw-w64-x86_64-gtk3` to install gtk3.
4. Add the directory `C:/msys64/mingw64/bin` to your PATH.

Then, follow the gir.core instructions as normal.

### Other Libraries
In order to obtain other libraries (e.g. libhandy), use the
[MSYS2 Package Database](https://packages.msys2.org/updates)
to find the desired library.

For example, libhandy can be found in `mingw-w64-libhandy`. Unfortunately,
not all libraries in gir.core have msys2 packages at the moment.

## MSVC
Gtk can also be built with the MSVC toolchain, although this is less
straightforward. Look at [gvs-build](https://github.com/wingtk/gvsbuild)
for a build script that automates this process.

## Troubleshooting
### System.DllNotFoundException
The DLL could not be found. Make sure it is in your PATH and try again. Try
adding `C:/msys64/mingw64/bin` at the **front** of your PATH if you have other
Gtk applications installed - this prevents the wrong version of the Gtk DLL
being loaded by the bindings.