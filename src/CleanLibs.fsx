#r "nuget: SimpleExec, 8.0.0"
open SimpleExec

Command.Run("dotnet", "run --project GirTool/GirTool.csproj -- clean Libs")