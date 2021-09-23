namespace GirLoader.Output
{
    public class Short : PrimitiveValueType, GirModel.Short
    {
        public Short(string ctype) : base(new CType(ctype), new TypeName("short")) { }
    }
}
