using System;
using System.IO;
using Repository.Services;

namespace Repository.Xml
{
    internal class Loader
    {
        private readonly XmlService _xmlService;

        public Loader(XmlService xmlService)
        {
            _xmlService = xmlService;
        }

        public Repository LoadRepository(GirFile target)
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
