using Generator3.Generation;

namespace Generator3.Publication
{
    public class NativeDelegateFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Native", "Delegates");
        }
    }
}
