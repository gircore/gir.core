using System.Collections.Generic;

namespace Repository.Model
{
    public interface TypeReferenceProvider
    {
        IEnumerable<TypeReference> GetTypeReferences();
    }
}
