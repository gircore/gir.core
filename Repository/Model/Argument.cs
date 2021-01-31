using Repository.Analysis;

namespace Repository.Model
{
    public record Argument(
        string Name, 
        ITypeReference Type, 
        Direction Direction, 
        Transfer Transfer, 
        bool Nullable
    );
}
