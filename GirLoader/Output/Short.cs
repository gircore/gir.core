namespace GirLoader.Output
{
    public class Short : PrimitiveValueType
    {
        public Short(string ctype) : base(new CType(ctype), new TypeName("short")) { }
    }
}
