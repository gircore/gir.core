namespace Generator3.Model.Internal
{
    public class VoidParameter : Parameter
    {
        public override string NullableTypeName => TypeMapping.Pointer;

        protected internal VoidParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyTypeReference.AnyType.VerifyType<GirModel.Void>();
        }
    }
}
