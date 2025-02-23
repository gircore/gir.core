using System;

namespace GirLoader.Output;

public partial class Alias : Type
{
    public string Name { get; }
    public new string CType => base.CType ?? throw new Exception($"Alias {Name} is missing a ctype");
    public Repository Repository { get; }
    public TypeReference TypeReference { get; }

    public Alias(Repository repository, string name, string cType, TypeReference typeReference) : base(cType)
    {
        TypeReference = typeReference;
        Repository = repository;
        Name = name;
    }

    public override string ToString()
        => CType;

    internal override bool Matches(TypeReference typeReference)
    {
        if (typeReference.CTypeReference?.CType is not null)
            return typeReference.CTypeReference.CType == CType;//Prefer CType

        if (typeReference.SymbolNameReference is not null)
        {
            var nameMatches = typeReference.SymbolNameReference.SymbolName == Name;
            var namespaceMatches = typeReference.SymbolNameReference.NamespaceName == Repository.Namespace.Name;
            var namespaceMissing = typeReference.SymbolNameReference.NamespaceName == null;

            return nameMatches && (namespaceMatches || namespaceMissing);
        }

        return false;
    }
}
