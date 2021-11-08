namespace Generator3.Model.Native
{
    public class VoidParameter : Parameter
    {
        public override string NullableTypeName => TypeMapping.Pointer;

        protected internal VoidParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Void>();
        }
    }
}
