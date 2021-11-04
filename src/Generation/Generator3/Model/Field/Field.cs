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
    }
}
