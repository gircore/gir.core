namespace Generator3.Publication.Filesystem
{
    internal class EnumFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Enums");
        }
    }
}
