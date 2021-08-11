namespace GirLoader.Output
{
    public class Boolean : PrimitiveValueType
    {
        public Boolean(string ctype) : base(new CType(ctype), new TypeName("bool")) { }
    }
}
