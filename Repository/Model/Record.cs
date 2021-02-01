using Repository.Analysis;

namespace Repository.Model
{
    public record Record : IType
    {
        public Namespace Namespace { get; init; }
        public string ManagedName { get; set; }
        public string NativeName { get; init; }
        
        public ITypeReference? GLibClassStructFor { get; init; }
    }
}
