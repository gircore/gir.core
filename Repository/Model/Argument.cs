using Repository.Analysis;

namespace Repository.Model
{
    public class Argument
    {
        public string Name { get; }
        public ITypeReference TypeReference { get; }
        public Direction Direction { get; }
        public Transfer Transfer { get; }
        public bool Nullable { get; }

        public Argument(string name, ITypeReference typeReference, Direction direction, Transfer transfer, bool nullable)
        {
            Name = name;
            TypeReference = typeReference;
            Direction = direction;
            Transfer = transfer;
            Nullable = nullable;
        }
    }
}
