using System;

namespace GirLoader.Output.Model
{
    public class ResolveableTypeReference : TypeReference, Resolveable
    { 
        public Type? ResolvedType { get; private set; }

        public ResolveableTypeReference(SymbolName? originalName, CType? ctype, NamespaceName? namespaceName) 
            : base(originalName, ctype, namespaceName)
        {
        }

        public Type GetResolvedType()
        {
            if (ResolvedType is null)
                throw new InvalidOperationException($"The {nameof(TypeReference)} for {OriginalName} has not been resolved.");

            return ResolvedType;
        }

        public void ResolveAs(Type type)
        {
            ResolvedType = type;
        }

        public override bool GetIsResolved()
            => ResolvedType is { };
    }
}
