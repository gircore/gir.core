using System;

namespace GirLoader.Output;

public union AnyTypeReference(TypeReference, ArrayTypeReference)
{
    public static AnyTypeReference From(TypeReference typeReference) => typeReference;
    public CTypeReference? CTypeReference => ((TypeIdentifier) Value).CTypeReference;
    public SymbolNameReference? SymbolNameReference => ((TypeIdentifier) Value).SymbolNameReference;
    
    public TypeReference AsTypeReference => (TypeReference) Value;
    public ArrayTypeReference AsArrayTypeReference => (ArrayTypeReference) Value;
}

public class AnyTypeReference2 : OneOf.OneOfBase<TypeReference, ArrayTypeReference>, TypeIdentifier
{
    private AnyTypeReference2(OneOf.OneOf<TypeReference, ArrayTypeReference> input) : base(input) { }

    public static AnyTypeReference2 From(TypeReference typeReference) => new(OneOf.OneOf<TypeReference, ArrayTypeReference>.FromT0(typeReference));
    public static AnyTypeReference2 From(ArrayTypeReference arrayTypeReference) => new(OneOf.OneOf<TypeReference, ArrayTypeReference>.FromT1(arrayTypeReference));

    public CTypeReference? CTypeReference => ((TypeIdentifier) Value).CTypeReference;
    public SymbolNameReference? SymbolNameReference => ((TypeIdentifier) Value).SymbolNameReference;

    public static implicit operator AnyTypeReference2(TypeReference typeReference) => From(typeReference);
    public static implicit operator AnyTypeReference2(ArrayTypeReference arrayTypeReference) => From(arrayTypeReference);
}
