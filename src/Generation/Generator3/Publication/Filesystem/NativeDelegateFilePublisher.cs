namespace Generator3.Publication.Filesystem
{
    internal class NativeDelegateFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Native", "Delegates");
        }
    }
}
