namespace Generator3.Model.Internal
{
    public class PointerRecordParameter : Parameter
    {
        public override string NullableTypeName => TypeMapping.Pointer;

        protected internal PointerRecordParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.Record>();
        }
    }
}
