using System.Collections.Generic;
using System.IO;
using Repository.Analysis;
using Repository.Model;
using StrongInject;

namespace Repository
{
    public class Test
    {
        public static void Testi(FileInfo fileInfo)
        {
            (Namespace, IEnumerable<TypeReference>) r = new Container().Run((p) => p.Parse(fileInfo));
        }
    }
}
