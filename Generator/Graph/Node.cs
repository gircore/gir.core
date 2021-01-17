using System.Collections.Generic;

namespace Generator.Graph
{
    public interface INode
    {
        public List<INode> Dependencies { get; }
        public string Name { get; }
    }
}
