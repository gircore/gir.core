using System.Collections.Generic;

namespace Repository.Model
{
    public class Repository
    {
        public Namespace Namespace { get; }
        public IEnumerable<Include> Includes { get; }

        public Repository(Namespace @namespace, IEnumerable<Include> includes)
        {
            Namespace = @namespace;
            Includes = includes;
        }
    }
}
