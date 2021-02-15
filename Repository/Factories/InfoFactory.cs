using System;
using System.Collections.Generic;
using Repository.Xml;

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
        {
            if (namespaceInfo.Name is null || namespaceInfo.Version is null)
                throw new Exception("Can't create info because data is missing");
            
            return new Info(namespaceInfo.Name, namespaceInfo.Version);
        }

        public IEnumerable<Info> CreateFromIncludes(IEnumerable<IncludeInfo> includes)
        {
            foreach (IncludeInfo includeInfo in includes)
            {
                if (includeInfo.Name is null || includeInfo.Version is null)
                    throw new Exception("Can't create info because data is missing");
                
                yield return new Info(includeInfo.Name, includeInfo.Version);
            }
        }
    }
}
