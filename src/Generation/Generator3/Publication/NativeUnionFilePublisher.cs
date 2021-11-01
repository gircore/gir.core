using Generator3.Generation;

namespace Generator3.Publication
{
    public class NativeUnionFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Native", "Unions");
        }
    }
}
