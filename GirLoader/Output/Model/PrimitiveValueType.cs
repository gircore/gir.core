namespace GirLoader.Output.Model
{
    public abstract class PrimitiveValueType : PrimitiveType
    {
        protected PrimitiveValueType(CType cType, SymbolName symbolName) : base(cType, symbolName)
        {
        }
    }
}
