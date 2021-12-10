namespace GirLoader.Output
{
    public class Float : PrimitiveValueType, GirModel.Float
    {
        public Float(string ctype) : base(new CType(ctype)) { }
    }
}
