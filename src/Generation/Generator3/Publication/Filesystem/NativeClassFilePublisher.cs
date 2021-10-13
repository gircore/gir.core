namespace Generator3.Publication.Filesystem
{
    internal class NativeClassFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Native", "Classes");
        }
    }
}
