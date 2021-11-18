using GirModel;

namespace Generator3.Model.Public
{
    public class StandardParameter
    {
        private readonly Parameter _parameter;

        public string Name => _parameter.Name;
        
        public string NullableTypeName => _parameter.AnyTypeReference.AnyType.Match(
            type => type.GetName() + GetDefaultNullable(),
            arrayType => arrayType.GetName() //TODO: Consider if StandardParameter should support arrays?
        );

        public string Direction => _parameter.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal StandardParameter(GirModel.Parameter parameter)
        {
            _parameter = parameter;
        }
        
        protected string GetDefaultNullable() => _parameter.Nullable ? "?" : "";
    }
}
