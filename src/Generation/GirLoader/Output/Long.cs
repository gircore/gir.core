namespace GirLoader.Output
{
    public class Long : PrimitiveValueType, GirModel.Long
    {
        public Long(string ctype) : base(new CType(ctype), new TypeName("long")) { }
    }
}
