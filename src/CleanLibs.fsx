#r "nuget: SimpleExec, 8.0.0"
open SimpleExec

Command.Run("dotnet", "run --project Generation/GirTool/GirTool.csproj -- clean Libs")