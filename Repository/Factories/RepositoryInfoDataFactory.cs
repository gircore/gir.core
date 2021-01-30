using System.Collections.Generic;
using Repository.Xml;

#nullable enable

namespace Repository.Services
{
    public record RepositoryInfoData(string Name, string Version)
    {
        public string ToCanonicalName()
            => $"{Name}-{Version}";
    };

    public interface IRepositoryInfoDataFactory
    {
        IEnumerable<RepositoryInfoData> GetDependencies(RepositoryInfo repositoryInfo);

        RepositoryInfoData GetData(NamespaceInfo repositoryInfo);
    }

    public class RepositoryInfoDataFactory : IRepositoryInfoDataFactory
    {
        public RepositoryInfoData GetData(NamespaceInfo namespaceInfo)
            => new RepositoryInfoData(namespaceInfo.Name, namespaceInfo.Version);
        
        public IEnumerable<RepositoryInfoData> GetDependencies(RepositoryInfo repoInfo)
        {
            foreach (IncludeInfo includeInfo in repoInfo.Includes)
            {
                yield return new RepositoryInfoData(includeInfo.Name, includeInfo.Version);
            }
        }
    }
}
