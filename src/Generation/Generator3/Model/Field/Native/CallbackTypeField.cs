namespace Generator3.Model.Native
{
    public class CallbackTypeField : Field
    {
        public override string? Attribute => null;

        public override string NullableTypeName => _field.AnyTypeOrCallback.AsT0.AsT0.GetName() + "Callback";

        public CallbackTypeField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyType<GirModel.Callback>();
        }
    }
}
