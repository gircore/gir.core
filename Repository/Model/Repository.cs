using System.Collections.Generic;
using System.Linq;
using Repository.Graph;

namespace Repository.Model
{
    public class Repository : INode<Repository>
    {
        public Namespace Namespace { get; }
        public IEnumerable<Include> Includes { get; }
        public IEnumerable<Repository> Dependencies => Includes.Select(x => x.ResolvedRepository);

        public Repository(Namespace @namespace, IEnumerable<Include> includes)
        {
            Namespace = @namespace;
            Includes = includes;
        }
    }
}
