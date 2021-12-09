using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public class CallbackField : Field
    {
        public override string NullableTypeName => _field.AnyTypeOrCallback.AsT1.GetName() + "Callback";
        
        public CallbackField(GirModel.Field field) : base(field)
        {
            
        }
    }
}
