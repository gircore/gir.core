using System;
using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;

namespace GirTool;

public class CleanCommand : Command
{
    public CleanCommand() : base(
        name: "clean",
        description: "Cleans the output directories")
    {
        AddArgument(new Argument<string>(
            name: "target",
            description: "Target folder to clean of all generated C# files (*.Generated.cs)"
        ));

        this.SetHandler<string, InvocationContext>(
            handle: Execute,
            symbols: this.Children.Cast<IValueDescriptor>().ToArray()
        );
    }

    private static void Execute(string folder, InvocationContext invocationContext)
    {
        try
        {
            if (!VerifyFolderExits(folder, invocationContext))
                return;

            var deletedFiles = 0;
            var searchedFolders = 0;

            foreach (var d in Directory.EnumerateDirectories(folder, "*", SearchOption.AllDirectories))
            {
                foreach (var file in Directory.EnumerateFiles(d).Where(FileIsGenerated))
                {
                    File.Delete(file);
                    deletedFiles++;
                }

                searchedFolders++;
            }

            Log.Information($"Deleted {deletedFiles} files in {searchedFolders} folders");
        }
        catch (Exception ex)
        {
            Log.Exception(ex);
            Log.Error("An error occurred while cleaning files. Please save a copy of your log output and open an issue at: https://github.com/gircore/gir.core/issues/new");
            invocationContext.ExitCode = 1;
        }
    }

    private static bool VerifyFolderExits(string folder, InvocationContext invocationContext)
    {
        if (Directory.Exists(folder))
            return true;

        Log.Error($"Folder {folder} does not exist");
        invocationContext.ExitCode = 1;
        return false;
    }

    private static bool FileIsGenerated(string file) => file.EndsWith(".Generated.cs");
}
