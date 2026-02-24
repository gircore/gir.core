using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public partial class Interface : ComplexType, AccessorProvider, ShadowableProvider
{
    private readonly List<Method> _methods;
    private readonly List<Function> _functions;
    private readonly List<Property> _properties;
    private readonly List<Signal> _signals;

    public Function GetTypeFunction { get; }
    public IEnumerable<TypeReference> Implements { get; }
    public IEnumerable<TypeReference> Prerequisites { get; }

    public IEnumerable<Constructor>? Constructors => null; //TODO: Not implemented yet
    public IEnumerable<Method> Methods => _methods;
    public IEnumerable<Function> Functions => _functions;
    public IEnumerable<Property> Properties => _properties;
    public IEnumerable<Signal> Signals => _signals;
    public bool Introspectable { get; }

    public Interface(Repository repository, string? cType, string name, IEnumerable<TypeReference> implements, IEnumerable<TypeReference> prerequisites, IEnumerable<Method> methods, IEnumerable<Function> functions, Function getTypeFunction, IEnumerable<Property> properties, IEnumerable<Signal> signals, bool introspectable) : base(repository, cType, name)
    {
        Prerequisites = prerequisites;
        Implements = implements;
        this._methods = methods.ToList();
        this._functions = functions.ToList();
        this._signals = signals.ToList();
        GetTypeFunction = getTypeFunction;
        _properties = properties.ToList();
        Introspectable = introspectable;
    }

    internal override bool Matches(TypeReference typeReference)
    {
        if (typeReference.CTypeReference is not null && typeReference.CTypeReference.CType != "gpointer")
            return typeReference.CTypeReference.CType == CType;

        if (typeReference.SymbolNameReference is not null)
        {
            var nameMatches = typeReference.SymbolNameReference.SymbolName == Name;
            var namespaceMatches = typeReference.SymbolNameReference.NamespaceName == Repository.Namespace.Name;
            var namespaceMissing = typeReference.SymbolNameReference.NamespaceName == null;

            return nameMatches && (namespaceMatches || namespaceMissing);
        }

        return false;
    }

    public override string ToString()
        => $"{Repository.Namespace.Name}.{Name}";
}
