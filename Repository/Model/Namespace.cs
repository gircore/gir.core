using System.Collections.Generic;
using Repository.Analysis;

namespace Repository.Model
{
    public class Namespace : ISymbolReferenceProvider
    {
        #region Properties
        
        // Basic Info
        public NamespaceName Name { get; }
        public string Version { get; }
        
        public string? SharedLibrary { get; }
        
        private readonly List<Symbol> _aliases = new();
        public IEnumerable<Symbol> Aliases => _aliases;
        
        private readonly List<Callback> _callbacks = new();
        public IEnumerable<Callback> Callbacks => _callbacks;

        private readonly List<Class> _classes = new();
        public IEnumerable<Class> Classes => _classes;

        private readonly List<Enumeration> _enumerations = new();
        public IEnumerable<Enumeration> Enumerations => _enumerations;

        private readonly List<Enumeration> _bitfields = new();
        public IEnumerable<Enumeration> Bitfields => _bitfields;

        private readonly List<Interface> _interfaces = new();
        public IEnumerable<Interface> Interfaces => _interfaces;

        private readonly List<Record> _records = new();
        public IEnumerable<Record> Records => _records;
        
        private readonly List<Method> _functions = new();
        public IEnumerable<Method> Functions => _functions;

        private readonly List<Record> _unions = new();
        public IEnumerable<Record> Unions => _unions;

        private readonly  List<Constant> _constants = new();
        public IEnumerable<Constant> Constants => _constants;

        #endregion

        public Namespace(string name, string version, string? sharedLibrary)
        {
            Name = new NamespaceName(name);
            Version = version;
            SharedLibrary = sharedLibrary;
        }

        internal void AddAlias(Symbol symbol)
            => _aliases.Add(symbol);

        internal void AddCallback(Callback callback)
            => _callbacks.Add(callback);

        internal void AddClass(Class @class)
            => _classes.Add(@class);

        internal void AddEnumeration(Enumeration enumeration)
            => _enumerations.Add(enumeration);

        internal void AddBitfield(Enumeration enumeration)
            => _bitfields.Add(enumeration);

        internal void AddInterface(Interface @interface)
            => _interfaces.Add(@interface);

        internal void AddRecord(Record @record)
            => _records.Add(@record);

        public void RemoveRecord(Record @record)
            => _records.Remove(@record);

        internal void AddFunction(Method method)
            => _functions.Add(method);

        internal void AddUnion(Record union)
            => _unions.Add(union);

        internal void AddConstant(Constant constant)
            => _constants.Add(constant);
        
        public IEnumerable<SymbolReference> GetSymbolReferences()
        {
            return IEnumerables.Concat(
                Aliases.GetSymbolReferences(),
                Callbacks.GetSymbolReferences(),
                Classes.GetSymbolReferences(),
                Enumerations.GetSymbolReferences(),
                Bitfields.GetSymbolReferences(),
                Interfaces.GetSymbolReferences(),
                Records.GetSymbolReferences(),
                Functions.GetSymbolReferences(),
                Unions.GetSymbolReferences(),
                Constants.GetSymbolReferences()
            );
        }

        public string ToCanonicalName() => $"{Name}-{Version}";
        
        internal void Strip()
        {
            Classes.Strip();
            Interfaces.Strip();

            _aliases.RemoveAll(Remove);
            _callbacks.RemoveAll(Remove);
            _classes.RemoveAll(Remove);
            _enumerations.RemoveAll(Remove);
            _bitfields.RemoveAll(Remove);
            _interfaces.RemoveAll(Remove);
            _records.RemoveAll(Remove);
            _functions.RemoveAll(Remove);
            _unions.RemoveAll(Remove);
            _constants.RemoveAll(Remove);
        }

        private bool Remove(Symbol symbol)
        {
            var result = symbol.GetIsResolved();
            
            if(!result)
                Log.Information($"{symbol.GetType().Name} {symbol.Namespace?.Name}.{symbol.Name}: Removed because parts of it could not be completely resolvled");

            return !result;
        }
    }
}
