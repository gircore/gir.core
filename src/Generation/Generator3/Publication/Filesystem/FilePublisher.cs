using System.Collections.Generic;
using System.IO;

namespace Generator3.Publication.Filesystem
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
            File.WriteAllText(path, codeUnit.Source);
        }
    }
}
