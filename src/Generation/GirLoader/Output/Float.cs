namespace GirLoader.Output
{
    public class Float : PrimitiveValueType
    {
        public Float(string ctype) : base(new CType(ctype), new TypeName("float")) { }
    }
}
