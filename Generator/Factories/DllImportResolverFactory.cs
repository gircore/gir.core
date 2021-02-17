#nullable enable

namespace Generator.Factories
{
    public interface IDllImportResolverFactory
    {
        IDllImportResolver Create(string sharedLibrary, string namespaceName);
    }

    public class DllImportResolverFactory : IDllImportResolverFactory
    {
        public IDllImportResolver Create(string sharedLibrary, string namespaceName)
        {
            return new DllImportResolver(sharedLibrary, namespaceName);
        }
    }
}
