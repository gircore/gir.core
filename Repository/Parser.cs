using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

using Repository.Xml;
using Repository.Model;

namespace Repository
{
    public class Parser
    {
        private readonly RepositoryInfo repoInfo;
        private readonly FileInfo girFile;
        
        public Parser(FileInfo girFile)
        {
            repoInfo = Deserialize(girFile);
        }
        
        public IEnumerable<(string, string)> GetDependencies()
        {
            foreach (IncludeInfo includeInfo in repoInfo.Includes)
            {
                yield return (includeInfo.Name, includeInfo.Version);
            }
        }

        public Namespace Parse()
        {
            if (repoInfo.Namespace == null)
                throw new InvalidDataException($"File '{girFile} does not define a namespace.");

            NamespaceInfo nspace = repoInfo.Namespace;

            return new Namespace()
            {
                Name = nspace.Name,
                Version = nspace.Version
            };
        }
        
        private static RepositoryInfo Deserialize(FileInfo girFile)
        {
            var serializer = new XmlSerializer(
                type: typeof(RepositoryInfo),
                defaultNamespace: "http://www.gtk.org/introspection/core/1.0");

            using FileStream fs = girFile.OpenRead();
            
            return (RepositoryInfo)serializer.Deserialize(fs);
        }
    }
}
