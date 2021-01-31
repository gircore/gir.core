using System.Collections.Generic;
using Repository.Xml;

#nullable enable

namespace Repository.Services
{
    public record Info(string Name, string Version)
    {
        public string ToCanonicalName()
            => $"{Name}-{Version}";
    };

    public interface IInfoFactory
    {
        IEnumerable<Info> CreateFromIncludes(IEnumerable<IncludeInfo> includes);

        Info CreateFromNamespaceInfo(NamespaceInfo repositoryInfo);
    }

    public class InfoFactory : IInfoFactory
    {
        public Info CreateFromNamespaceInfo(NamespaceInfo namespaceInfo)
            => new Info(namespaceInfo.Name, namespaceInfo.Version);
        
        public IEnumerable<Info> CreateFromIncludes(IEnumerable<IncludeInfo> includes)
        {
            foreach (IncludeInfo includeInfo in includes)
            {
                yield return new Info(includeInfo.Name, includeInfo.Version);
            }
        }
    }
}
