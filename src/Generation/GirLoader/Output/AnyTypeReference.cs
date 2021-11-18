namespace GirLoader.Output
{
    public class AnyTypeReference : GirModel.AnyTypeReference
    {
        private readonly TypeReference _typeReference;
        public bool IsPointer => _typeReference.CTypeReference?.IsPointer ?? false;

        public bool IsConst => _typeReference.CTypeReference?.IsConst ?? false;

        public bool IsVolatile => _typeReference.CTypeReference?.IsVolatile ?? false;
        
        public GirModel.AnyType AnyType => _typeReference switch
        {
            ArrayTypeReference arrayTypeReference => GirModel.AnyType.From(arrayTypeReference),
            _ => GirModel.AnyType.From(_typeReference.GetResolvedType())
        };

        public AnyTypeReference(TypeReference typeReference)
        {
            _typeReference = typeReference;
        }
    }
}
