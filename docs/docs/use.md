# Use
To use the bindings create a C# project and just add the corresponding [nuget packages](https://www.nuget.org/profiles/GirCore).

There are a lot of [sample projects](https://github.com/gircore/gir.core/tree/main/src/Samples) available to get you started. Extensive [API documentation](https://gircore.github.io/api/index.html) is provided and the [original documentation](https://developer.gnome.org/documentation/introduction/overview/libraries.html) can also be referenced.

The gir.core project is *not* providing the actual C libraries but only the C# bindings. Please ensure that the corresponding packages are installed on your system otherwise the binding will not find a target to bind to during runtime.

In case the packages are not installed on your system please refer to the documentation of your systems package manager.

## Running on Linux
In most distributions the needed packages should already be installed. In case something is missing use the package manager of your distribution to install the missing dependencies.

## Running on MacOS
Use the [Homebrew package database](https://formulae.brew.sh/) to find and install any needed packages.

## Running on Windows
The easiest way to get started on Windows is by installing the packages through msys2. The [MSYS2 Package Database](https://packages.msys2.org/) can be searched for matching packages.

1. Download msys2 from the [official website](https://www.msys2.org/).
2. Run `pacman -Syu` to update the package database.
3. Run `pacman -S mingw-w64-x86_64-XXX` to install a package named XXX.
4. Add the directory `C:/msys64/mingw64/bin` to the front of the PATH.

## Troubleshooting
Common problems which can come up during development.

### DLL not found Exception
If the binaries are installed on the system but are not found during runtime a `DllNotFoundException` is raised. In this case the names of the installed libraries probably don't match the expected names.

The gir.core nuget packages are build against 3 different package sources:
1. Gnome SDK (Linux)
2. MSYS2 (Windows)
3. Homebrew (MacOS)

Each of those sources defines the names of the binaries which must be available to call into them. If a custom build binary is used, the resulting name of the binary can differ from the one specified by the package source, thus resulting in a `DllNotFoundException`.

In case of a custom build C binary it is recommended to use a custom gir.core build, too. Please follow the [build instructions](build.md) to get started. It is important to update the gir-files with the corresponding custom build gir-files.

This allows projects with custom build C binaries to create matching C# binaries without being dependent on one of the package sources.