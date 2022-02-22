namespace GirModel
{
    public interface Property
    {
        public string Name { get; }
        AnyType AnyType { get; }
        Transfer Transfer { get; }
        bool Readable { get; }
        bool Writeable { get; }
        bool Introspectable { get; }
        bool ConstructOnly { get; }
        //TODO add gobject setter function
        //TODO add gobject getter function
    }
}
