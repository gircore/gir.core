#r "nuget: SimpleExec, 8.0.0"
open SimpleExec

Command.Run(
    name = "dotnet",
    args = "run --project Generation/GirTool/GirTool.csproj -- clean Libs",
    workingDirectory = "../src"
)