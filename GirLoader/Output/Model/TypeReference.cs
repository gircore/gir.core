using System;

namespace GirLoader.Output.Model
{
    public class TypeReference : Resolveable
    {
        #region Properties

        public virtual Type? ResolvedType { get; private set; } //TODO virtual should be removed
        public NamespaceName? NamespaceName { get; }
        public CType? CType { get; }
        public SymbolName? OriginalName { get; }

        #endregion

        public TypeReference(SymbolName? originalName, CType? ctype, NamespaceName? namespaceName)
        {
            CType = ctype;
            OriginalName = originalName;
            NamespaceName = namespaceName;
        }

        public virtual Type GetResolvedType() //TODO virtual should be removed
        {
            if (ResolvedType is null)
                throw new InvalidOperationException($"The {nameof(TypeReference)} for {OriginalName} has not been resolved.");

            return ResolvedType;
        }

        public void ResolveAs(Type type)
        {
            ResolvedType = type;
        }

        public bool GetIsResolved()
            => ResolvedType is { };

        public override string ToString()
        {
            return CType.ToString();
        }
    }
}
