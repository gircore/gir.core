using Generator3.Converter;

namespace Generator3.Model.Internal
{
    public abstract class Field
    {
        protected readonly GirModel.Field _field;

        public string Name => _field.GetInternalName();
        public virtual string? Attribute { get; }
        
        public abstract string NullableTypeName { get; }
        
        protected Field(GirModel.Field field)
        {
            _field = field;
        }
    }
}
