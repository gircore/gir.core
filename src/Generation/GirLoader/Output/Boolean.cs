namespace GirLoader.Output
{
    public class Boolean : PrimitiveValueType, GirModel.Bool
    {
        public Boolean(string ctype) : base(new CType(ctype), new TypeName("bool")) { }
    }
}
