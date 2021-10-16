using GirModel;
using String = GirModel.String;

namespace Generator3.Model.Native
{
    public class StringField : Field
    {
        public override string? Attribute => _field.AnyTypeOrCallback.AsT0.AsT0 switch
        {
            PlatformString => "[MarshalAs(UnmanagedType.LPStr)]",
            String => "//TODO: Verify which attribute must be set here",
            _ => null
        };

        public override string NullableTypeName => _field.AnyTypeOrCallback.AsT0.AsT0.GetName();

        public StringField(GirModel.Field field) : base(field)
        {
            field.AnyTypeOrCallback.AsT0.VerifyType<String>();
        }
    }
}
