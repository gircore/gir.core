using System.IO;

namespace Generator3.Publication
{
    internal class FilePublisher
        : EnumerationPublisher,
            ConstantsPublisher,
            NativeFunctionsPublisher,
            NativeRecordFunctionsPublisher
    {
        public string TargetFolder { get; set; } = "../../Libs";

        void EnumerationPublisher.Publish(CodeUnit codeUnit)
        {
            var path = Path.Combine(TargetFolder, codeUnit.Project, "Enums", GetFileName(codeUnit));
            File.WriteAllText(path, codeUnit.Source);
        }

        void ConstantsPublisher.Publish(CodeUnit codeUnit)
        {
            var path = Path.Combine(TargetFolder, codeUnit.Project, "Classes", GetFileName(codeUnit));
            File.WriteAllText(path, codeUnit.Source);
        }

        void NativeFunctionsPublisher.Publish(CodeUnit codeUnit)
        {
            var path = Path.Combine(TargetFolder, codeUnit.Project, "Native", "Classes", GetFileName(codeUnit));
            File.WriteAllText(path, codeUnit.Source);
        }
        
        void NativeRecordFunctionsPublisher.Publish(CodeUnit codeUnit)
        {
            var path = Path.Combine(TargetFolder, codeUnit.Project, "Native", "Records", GetFileName(codeUnit));
            File.WriteAllText(path, codeUnit.Source);
        }

        private string GetFileName(CodeUnit codeUnit) => codeUnit.Name + ".Generated.cs";

    }
}
