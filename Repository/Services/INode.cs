using System.Collections.Generic;

namespace Repository.Graph
{
    internal interface INode<out T> where T : INode<T>
    {
        public IEnumerable<T> Dependencies { get; }
    }
}
