using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public class Repository
{
    private Namespace? _namespace;
    public Namespace Namespace => _namespace ?? throw new Exception("Namespace not initialized.");
    internal IEnumerable<Include> Includes { get; }
    internal IEnumerable<Repository> Dependencies => Includes.Select(x => x.GetResolvedRepository());

    public Repository() : this(Enumerable.Empty<Include>()) { }

    internal Repository(IEnumerable<Include> includes)
    {
        Includes = includes;
    }

    internal void SetNamespace(Namespace @namespace)
    {
        this._namespace = @namespace;
    }

    public override string ToString()
    {
        return $"{nameof(Repository)} for {Namespace.Name}";
    }
}
