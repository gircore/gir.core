# Build custom library

The `gir.core` nuget packages are built against 3 different package sources:
1. Gnome SDK (Linux)
2. MSYS2 (Windows)
3. Homebrew (MacOS)

Each of those sources defines the names of the binaries which must be available to call into them. If a custom build binary is used, the resulting binary name may be different from the one specified by the package source, resulting in a `System.DllNotFoundException`.

In case of a custom build C binary it is recommended to use a custom `gir.core` build, too. Please follow the [build instructions](build-source.md) to get started. It is important to update the gir-files with the corresponding custom build gir-files.

This allows projects with custom build C binaries to create matching C# binaries without being dependent on one of the package sources.
