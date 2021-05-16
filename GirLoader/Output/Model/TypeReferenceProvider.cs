using System.Collections.Generic;

namespace Gir.Output.Model
{
    public interface TypeReferenceProvider
    {
        IEnumerable<TypeReference> GetTypeReferences();
    }
}
