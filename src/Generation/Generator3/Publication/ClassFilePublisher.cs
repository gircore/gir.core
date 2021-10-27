using Generator3.Generation;

namespace Generator3.Publication
{
    public class ClassFilePublisher : FilePublisher, Publisher
    {
        public void Publish(CodeUnit codeUnit)
        {
            Publish(codeUnit, "Classes");
        }
    }
}
