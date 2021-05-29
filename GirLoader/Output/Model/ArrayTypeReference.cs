namespace GirLoader.Output.Model
{

    public class ArrayTypeReference : TypeReference
    {
        public int? Length { get; init; }
        public bool IsZeroTerminated { get; init; }
        public int? FixedSize { get; init; }
        public TypeReference TypeReference { get; }

        public ArrayTypeReference(TypeReference typeReference, SymbolName? originalName, CType? ctype, NamespaceName? namespaceName) : base(originalName, ctype, namespaceName)
        {
            TypeReference = typeReference;
        }

        public override bool GetIsResolved()
            => TypeReference.GetIsResolved();
    }
}
