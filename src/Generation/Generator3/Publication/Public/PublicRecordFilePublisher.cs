using Generator3.Generation;

namespace Generator3.Publication
{
    public class PublicRecordFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Records");
        }
    }
}
