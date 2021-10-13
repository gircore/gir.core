namespace Generator3.Publication.Filesystem
{
    internal class ClassFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Classes");
        }
    }
}
