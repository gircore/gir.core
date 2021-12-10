namespace GirLoader.Output
{
    public class Integer : PrimitiveValueType, GirModel.Integer
    {
        public Integer(string ctype) : base(new CType(ctype)) { }
    }
}
