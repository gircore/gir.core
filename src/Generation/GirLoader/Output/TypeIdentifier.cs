namespace GirLoader.Output;

public interface TypeIdentifier
{
    CTypeReference? CTypeReference { get; }
    SymbolNameReference? SymbolNameReference { get; }
}
