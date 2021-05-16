using System.Collections.Generic;

namespace GirLoader.Output.Model
{
    public interface TypeReferenceProvider
    {
        IEnumerable<TypeReference> GetTypeReferences();
    }
}
