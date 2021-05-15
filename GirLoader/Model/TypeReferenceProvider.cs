using System.Collections.Generic;

namespace Gir.Model
{
    public interface TypeReferenceProvider
    {
        IEnumerable<TypeReference> GetTypeReferences();
    }
}
