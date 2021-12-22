using Generator3.Generation;

namespace Generator3.Publication
{
    public class InternalUnionFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Internal", "Unions");
        }
    }
}
