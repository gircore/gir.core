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
        IEnumerable<Info> CreateFromRepositoryInfo(RepositoryInfo repositoryInfo);

        Info CreateFromNamespaceInfo(NamespaceInfo repositoryInfo);
    }

    public class InfoFactory : IInfoFactory
    {
        public Info CreateFromNamespaceInfo(NamespaceInfo namespaceInfo)
            => new Info(namespaceInfo.Name, namespaceInfo.Version);
        
        public IEnumerable<Info> CreateFromRepositoryInfo(RepositoryInfo repoInfo)
        {
            foreach (IncludeInfo includeInfo in repoInfo.Includes)
            {
                yield return new Info(includeInfo.Name, includeInfo.Version);
            }
        }
    }
}
