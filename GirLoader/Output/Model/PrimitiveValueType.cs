namespace GirLoader.Output.Model
{
    public abstract class PrimitiveValueType : PrimitiveType
    {
        protected PrimitiveValueType(CTypeName cTypeName, SymbolName symbolName) : base(cTypeName, symbolName)
        {
        }
    }
}
