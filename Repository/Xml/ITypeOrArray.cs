namespace Repository.Xml
{
    public interface ITypeOrArray
    {
        public TypeInfo Type { get; set; }
        public ArrayInfo Array { get; set; }
    }
}
