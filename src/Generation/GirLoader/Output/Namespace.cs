using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public partial class Namespace
{
    #region Fields
    private IEnumerable<Alias>? _aliases;
    private IEnumerable<Callback>? _callbacks;
    private IEnumerable<Class>? _classes;
    private IEnumerable<Enumeration>? _enumerations;
    private IEnumerable<Bitfield>? _bitfields;
    private IEnumerable<Interface>? _interfaces;
    private IEnumerable<Record>? _records;
    private IEnumerable<Function>? _functions;
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

    public IEnumerable<Function> Functions
    {
        get => _functions ??= Enumerable.Empty<Function>();
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

    public string ToCanonicalName() => $"{Name}-{Version}";

    public override string ToString()
        => Name;
}
