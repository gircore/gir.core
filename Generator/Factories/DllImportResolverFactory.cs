#nullable enable

namespace Generator.Factories
{
    public class DllImportResolverFactory
    {
        public DllImportResolver Create(string sharedLibrary, string namespaceName)
        {
            return new DllImportResolver(sharedLibrary, namespaceName);
        }
    }
}
