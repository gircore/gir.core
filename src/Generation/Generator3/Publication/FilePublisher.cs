using System;
using System.Collections.Generic;
using System.IO;
using Generator3.Generation;

namespace Generator3.Publication
{
    public abstract class FilePublisher
    {
        private string TargetFolder { get; set; } = "../../Libs";
        private string GetFileName(CodeUnit codeUnit) => codeUnit.Name + ".Generated.cs";

        protected void Publish(CodeUnit codeUnit, params string[] folders)
        {
            var allFolders = new List<string>();
            allFolders.Add(TargetFolder);
            allFolders.Add(codeUnit.Project);
            allFolders.AddRange(folders);
            allFolders.Add(GetFileName(codeUnit));

            var path = Path.Combine(allFolders.ToArray());
            var directoryName = Path.GetDirectoryName(path);

            if (directoryName is null)
                throw new Exception("Can not get directory for path: " + path);

            Directory.CreateDirectory(directoryName);
            File.WriteAllText(path, codeUnit.Source);
        }
    }
}
