using Repository.Analysis;

namespace Repository.Model
{
    public class ReturnValue
    {
        public ITypeReference TypeReference { get; }

        public ReturnValue(ITypeReference typeReference)
        {
            TypeReference = typeReference;
        }
    }
}
