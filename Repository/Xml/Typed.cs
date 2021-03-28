namespace Repository.Xml
{
    public interface Typed
    {
        public TypeInfo? Type { get; set; }
        public ArrayInfo? Array { get; set; }
    }
}
