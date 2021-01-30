using System;
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
            _xmlService = xmlService ?? throw new ArgumentNullException(nameof(xmlService));
            _namespaceInfoConverterService = namespaceInfoConverterService ?? throw new ArgumentNullException(nameof(namespaceInfoConverterService));
        }

        public (Namespace, IEnumerable<TypeReference>) Parse(FileInfo girFile)
        {
            var repoInfo = _xmlService.Deserialize<RepositoryInfo>(girFile);
            
            if (repoInfo.Namespace == null)
                throw new InvalidDataException($"File '{girFile} does not define a namespace.");

            return _namespaceInfoConverterService.Convert(repoInfo.Namespace);
        }
    }
}
