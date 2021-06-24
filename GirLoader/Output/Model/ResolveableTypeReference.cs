namespace GirLoader.Output.Model
{
    public class ResolveableTypeReference : TypeReference
    {
        private Type? _resolvedType;
        public override Type? ResolvedType => _resolvedType;

        public ResolveableTypeReference(SymbolName? originalName, CTypeReference? ctype)
            : base(originalName, ctype)
        {
        }

        public void ResolveAs(Type type)
        {
            _resolvedType = type;
        }
    }
}
