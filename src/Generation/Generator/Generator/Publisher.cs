using System;
using System.Collections.Generic;
using System.IO;

namespace Generator.Generator;

internal class Publisher
{
    private string TargetFolder { get; set; }

    public Publisher(string targetFolder)
    {
        TargetFolder = targetFolder;
    }

    public void Publish(CodeUnit codeUnit)
    {
        var allFolders = new List<string>
        {
            TargetFolder,
            codeUnit.Project,
            codeUnit.IsInternal ? "Internal" : "Public",
            GetFileName(codeUnit)
        };

        var path = Path.Combine(allFolders.ToArray());
        var directoryName = Path.GetDirectoryName(path);

        if (directoryName is null)
            throw new Exception("Can not get directory for path: " + path);

        Directory.CreateDirectory(directoryName);
        File.WriteAllText(path, codeUnit.Source);
    }

    private static string GetFileName(CodeUnit codeUnit) => codeUnit.Name + ".Generated.cs";
}
