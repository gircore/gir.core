using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GirLoader
{
    internal partial class TypeReferenceResolver
    {
        private abstract class TypeCache
        {
            private readonly HashSet<Output.Type> _types = new();

            public void Add(Output.Type type)
            {
                _types.Add(type);
            }

            public bool TryResolve(Output.TypeReference typeReference, [MaybeNullWhen(false)] out Output.Type type)
            {
                type = _types.FirstOrDefault(type => type.Matches(typeReference));
                return type is not null;
            }
        }
    }
}
