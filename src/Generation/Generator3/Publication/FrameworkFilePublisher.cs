using Generator3.Generation;

namespace Generator3.Publication
{
    public class FrameworkFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Framework");
        }
    }
}
