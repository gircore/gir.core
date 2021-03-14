namespace Repository.Xml
{
    internal interface ITypeOrArray
    {
        public TypeInfo? Type { get; set; }
        public ArrayInfo? Array { get; set; }
    }
}
