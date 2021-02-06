using Repository.Model;

namespace Repository.Analysis
{
    public enum ReferenceType
    {
        Internal,
        External
    }

    public interface ISymbolReference
    {
        ISymbol? Symbol { get;  }
        bool IsExternal { get;  }
        bool IsArray { get; }
        string Name { get; }
    }

    internal interface IResolveableSymbolReference : ISymbolReference
    {
        void ResolveAs(ISymbol symbol, ReferenceType referenceType);
    }
    
    public class SymbolReference : IResolveableSymbolReference
    {
        public ISymbol? Symbol { get; private set; }
        public bool IsExternal { get; private set; }
        public bool IsArray { get; }
        public string Name { get; }

        public SymbolReference(string name, bool isArray)
        {
            Name = name;
            IsArray = isArray;
        }

        public void ResolveAs(ISymbol symbol, ReferenceType referenceType)
        {
            Symbol = symbol;
            IsExternal = (referenceType == ReferenceType.External);
        }
    }
}
