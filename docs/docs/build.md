# Build

To generate the bindings locally execute the following commands in a terminal. Make sure to initialise submodules with `--recursive` otherwise the `gir-files` directory will not be loaded properly.

```sh
$ git clone --recursive https://github.com/gircore/gir.core.git
$ cd gir.core/scripts
$ dotnet fsi GenerateLibs.fsx
$ cd ../src
$ dotnet build GirCore.Libs.slnf
```

If you want to clean the [Libs folder](https://github.com/gircore/gir.core/tree/main/src/Libs) of all generated files run inside the scripts folder:

    $ dotnet fsi CleanLibs.fsx

## Native Libraries

The project includes a C library ([GirTest](https://github.com/gircore/gir.core/tree/main/src/Native/GirTestLib)) which is used for testing the generator.

This requires some additional dependencies:
- C compiler
- [meson](https://mesonbuild.com/SimpleStart.html)
- `gobject-introspection` (for Ubuntu, the `libgirepository1.0-dev` package is required)

To compile the `GirTest` native library, run:

    $ dotnet fsi GenerateGirTestLib.fsx

Then, to include the `GirTest` library when generating the bindings, run:

    $ dotnet fsi GenerateLibs.fsx GirTest-0.1.gir

To compile all the generated bindings, including `GirTest`, run:

    $ dotnet build

The `GirCore.slnx` solution contains all libraries, including `GirTest`.
The `GirCore.Libs.slnf` solution filter excludes the `GirTest` library's bindings and its unit tests, which can be useful if you have not built the native library.
