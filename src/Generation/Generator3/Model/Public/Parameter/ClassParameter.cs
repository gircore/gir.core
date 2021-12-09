using Generator3.Converter;

namespace Generator3.Model.Public
{
    public class ClassParameter : Parameter
    {
        private GirModel.Class Type => (GirModel.Class) Model.AnyTypeReference.AnyType.AsT0;
        
        public override string NullableTypeName => Type.GetFullyQualified() + GetDefaultNullable();

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal ClassParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyType<GirModel.Class>();
        }
    }
}
