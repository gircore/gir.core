namespace GirLoader.Output.Model
{
    public abstract class TypeReference : Resolveable
    {
        #region Properties

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

        public override string ToString()
        {
            return CType.ToString();
        }

        public abstract bool GetIsResolved();
    }
}
