namespace GirLoader.Output
{
    public class SymbolNameReference
    {
        public string SymbolName { get; }
        public NamespaceName? NamespaceName { get; }

        public SymbolNameReference(string symbolName, NamespaceName? namespaceName)
        {
            SymbolName = symbolName;
            NamespaceName = namespaceName;
        }
    }
}
