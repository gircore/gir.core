using System.Collections.Generic;
using System.Linq;
using GirLoader.Helper;

namespace GirLoader.Output
{
    public class Namespace
    {
        #region Fields
        private IEnumerable<Alias>? _aliases;
        private IEnumerable<Callback>? _callbacks;
        private IEnumerable<Class>? _classes;
        private IEnumerable<Enumeration>? _enumerations;
        private IEnumerable<Bitfield>? _bitfields;
        private IEnumerable<Interface>? _interfaces;
        private IEnumerable<Record>? _records;
        private IEnumerable<Method>? _functions;
        private IEnumerable<Union>? _unions;
        private IEnumerable<Constant>? _constants;
        #endregion

        #region Properties

        public string? IdentifierPrefixes { get; }
        public string? SymbolPrefixes { get; }

        public NamespaceName NativeName => Name with { Value = Name.Value + ".Native" };
        public NamespaceName Name { get; }
        public string Version { get; }
        public string? SharedLibrary { get; }

        public IEnumerable<Alias> Aliases
        {
            get => _aliases ??= Enumerable.Empty<Alias>();
            init => _aliases = value;
        }

        public IEnumerable<Callback> Callbacks
        {
            get => _callbacks ??= Enumerable.Empty<Callback>();
            init => _callbacks = value;
        }

        public IEnumerable<Class> Classes
        {
            get => _classes ??= Enumerable.Empty<Class>();
            init => _classes = value;
        }

        public IEnumerable<Enumeration> Enumerations
        {
            get => _enumerations ??= Enumerable.Empty<Enumeration>();
            init => _enumerations = value;
        }

        public IEnumerable<Bitfield> Bitfields
        {
            get => _bitfields ??= Enumerable.Empty<Bitfield>();
            init => _bitfields = value;
        }

        public IEnumerable<Interface> Interfaces
        {
            get => _interfaces ??= Enumerable.Empty<Interface>();
            init => _interfaces = value;
        }

        public IEnumerable<Record> Records
        {
            get => _records ??= Enumerable.Empty<Record>();
            init => _records = value;
        }

        public IEnumerable<Method> Functions
        {
            get => _functions ??= Enumerable.Empty<Method>();
            init => _functions = value;
        }

        public IEnumerable<Union> Unions
        {
            get => _unions ??= Enumerable.Empty<Union>();
            init => _unions = value;
        }

        public IEnumerable<Constant> Constants
        {
            get => _constants ??= Enumerable.Empty<Constant>();
            init => _constants = value;
        }

        #endregion

        public Namespace(string name, string version, string? sharedLibrary, string? identifierPrefixes, string? symbolPrefixes, Repository repository)
        {
            Name = new NamespaceName(name);
            Version = version;
            SharedLibrary = sharedLibrary;
            IdentifierPrefixes = identifierPrefixes;
            SymbolPrefixes = symbolPrefixes;

            repository.SetNamespace(this);
        }

        public IEnumerable<TypeReference> GetTypeReferences()
        {
            return IEnumerables.Concat(
                // It is important to keep aliases in the first position
                // as they must be resolved before all other types
                // as those can depend on the aliases and require
                // them to be resolved.
                Aliases.SelectMany(x => x.GetTypeReferences()),
                Callbacks.SelectMany(x => x.GetTypeReferences()),
                Classes.SelectMany(x => x.GetTypeReferences()),
                Enumerations.SelectMany(x => x.GetTypeReferences()),
                Bitfields.SelectMany(x => x.GetTypeReferences()),
                Interfaces.SelectMany(x => x.GetTypeReferences()),
                Records.SelectMany(x => x.GetTypeReferences()),
                Functions.SelectMany(x => x.GetTypeReferences()),
                Unions.SelectMany(x => x.GetTypeReferences()),
                Constants.SelectMany(x => x.GetTypeReferences())
            );
        }

        public string ToCanonicalName() => $"{Name}-{Version}";

        internal void Strip()
        {
            Classes.Strip();
            Interfaces.Strip();

            _aliases = Aliases.ToList().Where(IsResolved);
            _callbacks = Callbacks.ToList().Where(IsResolved);
            _classes = Classes.ToList().Where(IsResolved);
            _enumerations = Enumerations.ToList().Where(IsResolved);
            _bitfields = Bitfields.ToList().Where(IsResolved);
            _interfaces = Interfaces.ToList().Where(IsResolved);
            _records = Records.ToList().Where(IsResolved);
            _functions = Functions.ToList().Where(IsResolved);
            _unions = Unions.ToList().Where(IsResolved);
            _constants = Constants.ToList().Where(IsResolved);
        }

        private bool IsResolved(Alias alias)
        {
            var isResolved = alias.GetIsResolved();

            if (!isResolved)
                Log.Information($"{nameof(Alias)} {alias.Name}: Removed because parts of it could not be completely resolvled");

            return isResolved;
        }

        private bool IsResolved(Symbol symbol)
        {
            var isResolved = symbol.GetIsResolved();

            if (!isResolved)
                Log.Information($"{symbol.GetType().Name} {symbol.OriginalName}: Removed because parts of it could not be completely resolvled");

            return isResolved;
        }

        private bool IsResolved(ComplexType type)
        {
            var isResolved = type.GetIsResolved();

            if (!isResolved)
                Log.Information($"{type.GetType().Name} {type.Repository.Namespace.Name}.{type.Name}: Removed because parts of it could not be completely resolvled");

            return isResolved;
        }

        public override string ToString()
            => Name;
    }
}
