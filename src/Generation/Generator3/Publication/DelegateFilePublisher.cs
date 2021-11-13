using Generator3.Generation;

namespace Generator3.Publication
{
    public class DelegateFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Delegates");
        }
    }
}
