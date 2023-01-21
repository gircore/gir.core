#r "nuget: SimpleExec, 8.0.0"
open SimpleExec

let libSrcDir = System.IO.Path.Combine(
    System.IO.Directory.GetCurrentDirectory(),
    "Native/GirTestLib")
let libBuildDir = System.IO.Path.Combine(libSrcDir, "builddir")
let libInstallDir = System.IO.Path.Combine(libSrcDir, "installdir")

let girFileName = "GirTest-0.1.gir"
let girFilePath = System.IO.Path.Combine(libInstallDir, "share/gir-1.0", girFileName)

let extGirFileDir = System.IO.Path.Combine(
    System.IO.Directory.GetCurrentDirectory(),
    "../ext/gir-files")

(* Configure the project.
   We explicitly set the install folder to 'lib' to have a consistent path on all platforms
   - the default for Debian-like systems is 'lib/x86_64-linux-gnu'
   - the default on Windows is to install the dll to the 'bin' folder
*)
let wipeArg = if System.IO.Directory.Exists(libBuildDir) then "--wipe" else ""
Command.Run(
    name = "meson",
    args = $"setup {wipeArg} builddir --prefix {libInstallDir} --libdir lib --bindir lib",
    workingDirectory = libSrcDir
)

(* Compile and install to libInstallDir *)
Command.Run(
    name = "meson",
    args = "install -C builddir",
    workingDirectory = libSrcDir
)

(* Copy generated bindings to the gir files directory *)
for platform in [| "windows"; "macos"; "linux" |] do
    let destPath = System.IO.Path.Combine(extGirFileDir, platform, girFileName)
    System.IO.File.Copy(girFilePath, destPath, true)
    printfn $"Installed {destPath}"
