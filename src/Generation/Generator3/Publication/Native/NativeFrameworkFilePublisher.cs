using Generator3.Generation;

namespace Generator3.Publication
{
    public class NativeFrameworkFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Native", "Framework");
        }
    }
}
