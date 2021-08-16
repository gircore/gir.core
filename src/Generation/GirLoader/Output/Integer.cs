namespace GirLoader.Output
{
    public class Integer : PrimitiveValueType
    {
        public Integer(string ctype) : base(new CType(ctype), new TypeName("int")) { }
    }
}
