using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public partial class Record : ComplexType, ShadowableProvider
{
    private readonly List<Method> _methods;
    private readonly List<Function> _functions;
    private readonly List<Constructor> _constructors;
    private readonly List<Field> _fields;

    public bool IsClassStruct => GLibClassStructFor is { };

    public Function? GetTypeFunction { get; }
    public IEnumerable<Field> Fields => _fields;
    public bool Disguised { get; }
    public IEnumerable<Method> Methods => _methods;
    public IEnumerable<Constructor> Constructors => _constructors;
    public IEnumerable<Function> Functions => _functions;
    public TypeReference? GLibClassStructFor { get; }
    public bool Introspectable { get; }
    public bool Foreign { get; }
    public bool Opaque { get; }
    public bool Pointer { get; }
    public Method? CopyFunction { get; }
    public Method? FreeFunction { get; }

    public Record(Repository repository, string? cType, string name, TypeReference? gLibClassStructFor, IEnumerable<Method> methods, IEnumerable<Function> functions, Function? getTypeFunction, IEnumerable<Field> fields, bool disguised, IEnumerable<Constructor> constructors, bool introspectable, bool foreign, bool opaque, bool pointer, Method? copyFunction, Method? freeFunction) : base(repository, cType, name)
    {
        GLibClassStructFor = gLibClassStructFor;
        GetTypeFunction = getTypeFunction;
        Disguised = disguised;
        Introspectable = introspectable;
        Foreign = foreign;
        Opaque = opaque;
        Pointer = pointer;

        CopyFunction = copyFunction;
        FreeFunction = freeFunction;

        this._constructors = constructors.ToList();
        this._methods = methods.ToList();
        this._functions = functions.ToList();
        this._fields = fields.ToList();
    }

    internal override bool Matches(TypeReference typeReference)
    {
        var ctypeMatches = typeReference.CTypeReference?.CType == CType;
        var symbolNameMatches = typeReference.SymbolNameReference?.SymbolName == Name;
        var namespaceMatches = typeReference.SymbolNameReference?.NamespaceName == Repository.Namespace.Name;
        var namespaceMissing = typeReference.SymbolNameReference?.NamespaceName == null;

        return ctypeMatches || (symbolNameMatches && (namespaceMatches || namespaceMissing));
    }

    public override string ToString()
        => $"{Repository.Namespace.Name}.{Name}";
}
