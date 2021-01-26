using Repository.Analysis;

namespace Repository.Model
{
    public record Argument
    {
        public string Name { get; set; }
        public TypeReference Type { get; init; }
        public Direction Direction { get; init; }
        public Transfer Transfer { get; init; }
        public bool Nullable { get; init; }
    }
}
