using System;
using System.IO;
using Gir.Services;

namespace Gir.Xml
{
    internal class Loader
    {
        private readonly XmlService _xmlService;

        public Loader(XmlService xmlService)
        {
            _xmlService = xmlService;
        }

        public Repository LoadRepository(File target)
        {
            var file = new FileInfo(target.Path);
            Repository? repoInfo = _xmlService.Deserialize<Repository>(file);

            if (repoInfo is null)
                throw new Exception($"File {target} could not be deserialized");

            if (repoInfo.Namespace == null)
                throw new InvalidDataException($"File '{target} does not define a namespace.");

            return repoInfo;
        }
    }
}
