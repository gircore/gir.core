using Generator3.Generation;

namespace Generator3.Publication
{
    public class NativeInterfaceFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Native", "Interfaces");
        }
    }
}
