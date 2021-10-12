namespace Generator3.Generation.Unit.Native.RecordFunctions
{
    public class Model
    {
        public string Name { get; }
        public string NamespaceName { get; }

        public Model(string name, string namespaceName)
        {
            Name = name;
            NamespaceName = namespaceName;
        }
    }
}
