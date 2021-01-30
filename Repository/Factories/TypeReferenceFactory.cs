using Repository.Analysis;
using Repository.Xml;

#nullable enable

namespace Repository.Services
{
    public interface ITypeReferenceFactory
    {
        TypeReference Create(string type, bool isArray);
        TypeReference Create(ITypeOrArray? typeOrArray);
    }

    public class TypeReferenceFactory : ITypeReferenceFactory
    {
        public TypeReference Create(string type, bool isArray)
        {
            return new TypeReference(type, false);
        }
        
        public TypeReference Create(ITypeOrArray? typeOrArray)
        {
            // Check for Type
            var type = typeOrArray?.Type?.Name ?? null;
            if (type != null)
                return Create(type, false);

            // Check for Array
            var array = typeOrArray?.Array?.Type?.Name ?? null;
            if (array != null)
                return Create(array, true);

            // No Type (i.e. void)
            return Create("none", false);
        }
    }
}
