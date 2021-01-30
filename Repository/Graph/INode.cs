using System.Collections.Generic;

namespace Repository.Graph
{
    public interface INode
    {
        public List<INode> Dependencies { get; }
        public string Name { get; }
    }
}
