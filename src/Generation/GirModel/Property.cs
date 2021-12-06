namespace GirModel
{
    public interface Property
    {
        public string ManagedName { get; } //TODO Remove as nameing conversion should be part of the generator
        public string NativeName { get; }
        AnyType AnyType { get; }
        Transfer Transfer { get; }
        bool Readable { get; }
        bool Writeable { get; }
        
        //TODO add gobject setter function
        //TODO add gobject getter function
    }
}
