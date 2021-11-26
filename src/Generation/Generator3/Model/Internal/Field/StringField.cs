using String = GirModel.String;

namespace Generator3.Model.Internal
{
    public class StringField : Field
    {
        public override string? Attribute => MarshalAs.UnmanagedLpString();

        public override string NullableTypeName => _field.AnyTypeOrCallback.AsT0.AsT0.GetName();

        public StringField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyType<String>();
        }
    }
}
