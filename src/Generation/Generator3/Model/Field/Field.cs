using Generator3.Model.Native;
using GirModel;
using String = GirModel.String;

namespace Generator3.Model
{
    public abstract class Field
    {
        protected readonly GirModel.Field _field;

        public string Name => _field.Name;
        public abstract string? Attribute { get; }
        
        public abstract string NullableTypeName { get; }
        
        protected Field(GirModel.Field field)
        {
            _field = field;
        }
        
        public static Field CreateNative(GirModel.Field field) => field.AnyTypeOrCallback.Match(
            anyType => anyType.Match<Field>(
                type => type switch
                { 
                    String => new StringField(field),
                    Callback => new CallbackTypeField(field),
                    _ => new StandardField(field)
                },
                arrayType => new ArrayField(field)
            ),
            callback => new CallbackField(field)
        );
    }
}
