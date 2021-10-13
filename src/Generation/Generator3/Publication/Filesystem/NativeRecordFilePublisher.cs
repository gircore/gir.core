namespace Generator3.Publication.Filesystem
{
    internal class NativeRecordFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Native", "Records");
        }
    }
}
