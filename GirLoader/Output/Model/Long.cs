namespace GirLoader.Output.Model
{
    public class Long : PrimitiveValueType
    {
        public Long(string ctype) : base(new CType(ctype), new TypeName("long")) { }
    }
}
