namespace Generator.Introspection
{
    public interface IType
    {
        TypeInfo? Type { get; set; }

        ArrayInfo? Array { get; set; }

        public bool IsArray => Array is not null;
    }
}
