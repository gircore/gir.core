namespace GirLoader.Output;

public class AnyTypeReference : OneOf.OneOfBase<TypeReference, ArrayTypeReference>, TypeIdentifier
{
    private AnyTypeReference(OneOf.OneOf<TypeReference, ArrayTypeReference> input) : base(input) { }

    public static AnyTypeReference From(TypeReference typeReference) => new(OneOf.OneOf<TypeReference, ArrayTypeReference>.FromT0(typeReference));
    public static AnyTypeReference From(ArrayTypeReference arrayTypeReference) => new(OneOf.OneOf<TypeReference, ArrayTypeReference>.FromT1(arrayTypeReference));

    public CTypeReference? CTypeReference => ((TypeIdentifier) Value).CTypeReference;
    public SymbolNameReference? SymbolNameReference => ((TypeIdentifier) Value).SymbolNameReference;

    public static implicit operator AnyTypeReference(TypeReference typeReference) => From(typeReference);
    public static implicit operator AnyTypeReference(ArrayTypeReference arrayTypeReference) => From(arrayTypeReference);
}
