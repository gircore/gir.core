using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class CallbackField : Field
    {
        private GirModel.Callback _callback => _field.AnyTypeOrCallback.AsT1;

        public override string NullableTypeName => _callback.GetInternalName();

        public CallbackField(GirModel.Field field) : base(field)
        {

        }
    }
}
