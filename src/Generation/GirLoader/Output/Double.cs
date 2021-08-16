namespace GirLoader.Output
{
    public class Double : PrimitiveValueType
    {
        public Double(string ctype) : base(new CType(ctype), new TypeName("double")) { }
    }
}
