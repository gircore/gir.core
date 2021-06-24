namespace GirLoader.Output.Model
{

    public class ArrayTypeReference : TypeReference
    {
        public int? Length { get; init; }
        public bool IsZeroTerminated { get; init; }
        public int? FixedSize { get; init; }
        public TypeReference TypeReference { get; }

        public override Type? ResolvedType => TypeReference.ResolvedType;
        
        public ArrayTypeReference(TypeReference typeReference, SymbolName? originalName, CTypeReference? ctype) : base(originalName, ctype)
        {
            TypeReference = typeReference;
        }
    }
}
