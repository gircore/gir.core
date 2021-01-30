using System.Collections.Generic;
using System.IO;
using Repository.Xml;

namespace Repository.Services
{
    public interface IRepositoryInfoService
    {
        IEnumerable<(string, string)> GetDependencies(FileInfo girFile);
    }

    public class RepositoryInfoService : IRepositoryInfoService
    {
        private readonly IXmlService _xmlService;

        public RepositoryInfoService(IXmlService xmlService)
        {
            _xmlService = xmlService;
        }

        public IEnumerable<(string, string)> GetDependencies(FileInfo girFile)
        {
            var repoInfo = _xmlService.Deserialize<RepositoryInfo>(girFile);
            
            foreach (IncludeInfo includeInfo in repoInfo.Includes)
            {
                yield return (includeInfo.Name!, includeInfo.Version!);
            }
        }
    }
}
