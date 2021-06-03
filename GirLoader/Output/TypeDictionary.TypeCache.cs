using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GirLoader.Output
{
    internal partial class TypeDictionary
    {
        private class TypeCache
        {
            public Model.Repository? Repository { get; }
            private readonly HashSet<Model.Type> _types = new();

            public TypeCache(Model.Repository? repository)
            {
                Repository = repository;
            }

            public void Add(Model.Type type)
            {
                _types.Add(type);
            }

            public bool TryLookup(Model.TypeReference typeReference, [MaybeNullWhen(false)] out Model.Type type)
            {
                
                type = _types.FirstOrDefault(x => x.Matches(typeReference));

                if (type is null && typeReference.OriginalName.Value == "InitiallyUnowned")
                {
                    
                }
                
                return type is not null;
            }
        }
    }
}
