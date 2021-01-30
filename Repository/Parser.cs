using System.Collections.Generic;
using System.IO;
using Repository.Analysis;
using Repository.Model;
using Repository.Services;
using Repository.Xml;

#nullable enable

namespace Repository
{
    public class Parser
    {
        private readonly IXmlService _xmlService;
        private readonly INamespaceInfoConverterService _namespaceInfoConverterService;

        public Parser(IXmlService xmlService, INamespaceInfoConverterService namespaceInfoConverterService)
        {
            _xmlService = xmlService;
            _namespaceInfoConverterService = namespaceInfoConverterService;
        }

        public (Namespace, IEnumerable<ITypeReference>) Parse(FileInfo girFile)
        {
            var repoInfo = _xmlService.Deserialize<RepositoryInfo>(girFile);
            
            if (repoInfo.Namespace == null)
                throw new InvalidDataException($"File '{girFile} does not define a namespace.");

            return _namespaceInfoConverterService.Convert(repoInfo.Namespace);
        }
    }
}
