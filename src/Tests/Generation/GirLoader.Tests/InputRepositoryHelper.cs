namespace GirLoader.Test;

internal static class InputRepositoryHelper
{
    internal static Input.Repository CreateRepository(string namespaceName, string version)
    {
        return new Input.Repository()
        {
            Namespace = new Input.Namespace()
            {
                Name = namespaceName,
                Version = version
            }
        };
    }

    internal static string CreateXml(string namespaceName, string version)
    {
        return $"""
                <?xml version="1.0"?>
                <repository xmlns="http://www.gtk.org/introspection/core/1.0">
                  <namespace name="{namespaceName}" version="{version}">
                  </namespace>
                </repository>
                """;
    }
}
