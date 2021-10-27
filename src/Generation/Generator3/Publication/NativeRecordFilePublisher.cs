using Generator3.Generation;

namespace Generator3.Publication
{
    public class NativeRecordFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Native", "Records");
        }
    }
}
