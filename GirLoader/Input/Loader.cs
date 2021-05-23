using System;
using System.IO;

namespace GirLoader.Input
{
    internal class Loader
    {
        public Model.Repository LoadRepository(GirFile target)
        {
            var file = new FileInfo(target.Path);
            Model.Repository? repoInfo = Helper.Xml.Deserialize<Model.Repository>(file);

            if (repoInfo is null)
                throw new Exception($"File {target} could not be deserialized");

            if (repoInfo.Namespace == null)
                throw new InvalidDataException($"File '{target} does not define a namespace.");

            return repoInfo;
        }
    }
}
