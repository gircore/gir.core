using Generator3.Generation;

namespace Generator3.Publication
{
    public class InternalDelegateFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Internal", "Delegates");
        }
    }
}
