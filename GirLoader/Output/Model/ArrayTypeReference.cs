namespace GirLoader.Output.Model
{
    // TODO: ArrayTypeReference and TypeReference are not the same.
    // - TypeReference should get "ResolveableTypeReference"
    // - A new class TypeReference must be created which is the base for
    //   "ResolveableTypeReference" and "ArrayTypeReference"
    public class ArrayTypeReference : TypeReference
    {
        public int? Length { get; init; }
        public bool IsZeroTerminated { get; init; }
        public int? FixedSize { get; init; }
        public TypeReference TypeReference { get; }

        public override Type? ResolvedType => TypeReference.ResolvedType; //TODO Should get obsolete with new inheritance chain

        public ArrayTypeReference(TypeReference typeReference, SymbolName? originalName, CType? ctype, NamespaceName? namespaceName) : base(originalName, ctype, namespaceName)
        {
            TypeReference = typeReference;
        }

        public override Type GetResolvedType() //TODO Should get obsolete with new inheritance chain
        {
            return TypeReference.GetResolvedType();
        }
    }
}
