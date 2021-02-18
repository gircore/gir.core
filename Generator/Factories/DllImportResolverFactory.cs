#nullable enable

namespace Generator.Factories
{
    internal class DllImportResolverFactory
    {
        public DllImportResolver Create(string sharedLibrary, string namespaceName)
        {
            return new DllImportResolver(sharedLibrary, namespaceName);
        }
    }
}
