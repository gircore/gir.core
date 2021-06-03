namespace GirLoader.Output.Model
{
    public class ResolveableTypeReference : TypeReference
    {
        private Type? _resolvedType;
        public override Type? ResolvedType => _resolvedType;
        
        public ResolveableTypeReference(SymbolName? originalName, CType? ctype, NamespaceName? namespaceName) 
            : base(originalName, ctype, namespaceName)
        {
        }
        public void ResolveAs(Type type)
        {
            _resolvedType = type;
        }
    }
}
