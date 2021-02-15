using System.Collections.Generic;

namespace Repository.Graph
{
    public interface INode<out T> where T : INode<T>
    {
        public IEnumerable<T> Dependencies { get; }
        public string Name { get; }
    }
}
