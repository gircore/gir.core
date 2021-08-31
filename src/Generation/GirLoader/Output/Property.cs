using System.Collections.Generic;

namespace GirLoader.Output
{
    public interface Property : Transferable, Named
    {
        Transfer Transfer { get; init; }
        bool Writeable { get; init; }
        bool Readable { get; init; }
        Type Type { get; }
    }
    
    internal class TypeReferencingProperty : Symbol, Property, AnyType
    {
        private readonly TypeReference _typeReference;

        TypeReference AnyType.TypeReference => _typeReference;
        public Transfer Transfer { get; init; }
        public bool Writeable { get; init; }
        public bool Readable { get; init; }
        public Type? Type => _typeReference.Type;
        
        public TypeReferencingProperty(SymbolName originalName, SymbolName symbolName, TypeReference typeReference) : base(originalName, symbolName)
        {
            _typeReference = typeReference;
        }

        internal IEnumerable<TypeReference> GetTypeReferences()
        {
            yield return _typeReference;
        }

        internal bool GetIsResolved()
            => _typeReference.GetIsResolved();
    }
    
    public class TypedProperty : Symbol, Property
    {
        public Transfer Transfer { get; init; }
        public bool Writeable { get; init; }
        public bool Readable { get; init; }
        public Type Type { get; }

        public TypedProperty(SymbolName originalName, SymbolName symbolName, Type type) : base(originalName, symbolName)
        {
            Type = type;
        }
    }
}
