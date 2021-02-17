using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public interface  ISymbolReferenceProvider
    {
        IEnumerable<SymbolReference> GetSymbolReferences();
    }
    
    public interface ISymbol
    {
        string NativeName { get; }
        string ManagedName { get; }
    }

    public abstract class Symbol : ISymbol, ISymbolReferenceProvider
    {
        public string NativeName { get; }
        public string ManagedName { get; }

        public Symbol(string nativeName, string managedName)
        {
            NativeName = nativeName;
            ManagedName = managedName;
        }
        
        public abstract IEnumerable<SymbolReference> GetSymbolReferences();
    }
}
